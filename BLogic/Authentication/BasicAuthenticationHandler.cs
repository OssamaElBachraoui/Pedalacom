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
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Pedalacom.BLogic.Encryption;
using Pedalacom.Controllers;
using Pedalacom.Servizi.Log;

namespace Pedalacom.BLogic.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AdventureWorksLt2019Context _context;

        Log log;

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

                if (user != null && user.IsOld == 1)
                {
                    throw new InvalidOperationException("Autorizzazione non valida : Registrarsi nuovamente");

                }
                else {
                    DecryptSalt decryptSalt = new();

                    if (user == null || !decryptSalt.DecryptSaltCredential(user, password))
                    {
                        throw new InvalidOperationException("Autorizzazione non valida : Impossibile accedere al servizio");
                    }

                }
                var authenticationUser = new AuthenticationUser(username, "BasicAuthentication", true);
                var claims = new ClaimsPrincipal(new ClaimsIdentity(authenticationUser));

                return AuthenticateResult.Success(new AuthenticationTicket(claims, "BasicAuthentication"));
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return AuthenticateResult.Fail($"An error occurred: {ex.Message}");
                // return Task.FromResult(AuthenticateResult.Fail($"An error occurred: {ex.Message}"));
            }
        }


       

        

       

    }

}
