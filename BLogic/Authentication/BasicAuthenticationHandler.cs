using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Text;
using Pedalacom.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;
using System.Security.Cryptography;

namespace Pedalacom.BLogic.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AdventureWorksLt2019Context _context;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, AdventureWorksLt2019Context context) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                Response.Headers.Add("WWW-Authenticate", "Basic");

                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    throw new InvalidOperationException("Autorizzazione mancante: Impossibile accedere al servizio");
                }

                var authorizationHeader = Request.Headers["Authorization"].ToString();

                var authorizationRegEx = new Regex(@"Basic (.*)");

                if (!authorizationRegEx.IsMatch(authorizationHeader))
                {
                    throw new InvalidOperationException("Autorizzazione non valida : Impossibile accedere al servizio");
                }

                var authorizationBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationRegEx.Replace(authorizationHeader, "$1")));

                var authorizationSplit = authorizationBase64.Split(':', 2);

                if (authorizationSplit.Length != 2)
                {
                    throw new InvalidOperationException("Autorizzazione non valida : Impossibile accedere al servizio");
                }

                var username = authorizationSplit[0];
                var password = authorizationSplit[1];

                // Verifica nel database
                var user = await _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress.ToLower() == username.ToLower());

                if (user == null || VerifyPassword(user.PasswordHash, user.PasswordSalt, password))
                {
                    throw new InvalidOperationException("Autorizzazione non valida : Impossibile accedere al servizio");
                }

                var authenticaionUser = new AuthenticationUser(username, "BasicAuthentication", true);

                var claims = new ClaimsPrincipal(new ClaimsIdentity(authenticaionUser));

                return AuthenticateResult.Success(new AuthenticationTicket(claims, "BasicAuthentication"));
                //  return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claims, "BasicAuthentication")));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"An error occurred: {ex.Message}");
                // return Task.FromResult(AuthenticateResult.Fail($"An error occurred: {ex.Message}"));
            }
        }

        //private bool VerifyPassword(string hashedPassword, string salt, string password)
        //{
        //    // Concatenazione della password con il sale
        //    string passwordWithSalt = $"{password}{salt}";

        //    // Verifica utilizzando BCrypt
        //    return BCrypt.Net.BCrypt.Verify(passwordWithSalt, hashedPassword);
        //}

        private bool VerifyPassword(string hashedPassword, string salt, string password)
        {
            // Calcolare l'hash della password fornita con il salt
            string hashedInputPassword = HashPassword(password, salt);

            // Verifica se l'hash calcolato corrisponde all'hash memorizzato
            return string.Equals(hashedPassword, hashedInputPassword);
        }

        private string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Concatenazione della password con il salt
                string passwordWithSalt = $"{password}{salt}";

                // Calcolo dell'hash SHA-256
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));

                // Convertire i byte in una rappresentazione stringa esadecimale
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }

}
