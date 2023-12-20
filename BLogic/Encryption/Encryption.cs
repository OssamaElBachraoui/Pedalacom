using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Pedalacom.Servizi.Log;
using System.Security.Cryptography;
using System.Text;

namespace Pedalacom.Controllers
{
    public class Encryption
    {
        Log log;
        public string passM { get; }
        public Encryption()
        {
            
        }
        public KeyValuePair<string, string> EncrypSaltString(string sValue)
        {
            byte[] byteSalt = new byte[5];
            string encryptedResult = string.Empty;
            string encryptedSalt = string.Empty;

            try
            {
                RandomNumberGenerator.Fill(byteSalt);

                // P Password
                // S Salt
                // C COunt iterator
                // DKLEN (lunghezza della chiave
                encryptedResult = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: sValue,
                        salt: byteSalt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 16)
                        );

                encryptedSalt = Convert.ToBase64String(byteSalt);

            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                Console.WriteLine(ex.Message);
            }

            return new KeyValuePair<string, string>(encryptedResult, encryptedSalt);
        }


    }
}
