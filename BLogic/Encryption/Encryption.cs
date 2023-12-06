using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Pedalacom.Controllers
{
    public class Encryption
    {
        public string passM { get; }
        public Encryption()
        {
            
        }
        //private string Encrypt(string s)
        //{

        //    string EnString = "";
        //    try
        //    {

        //        SHA256 sha = SHA256.Create();

        //        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(s));

        //        StringBuilder stringBuilder = new StringBuilder();

        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            stringBuilder.Append(bytes[i].ToString("X2"));//x2 codifica valore in esadeciamle maiuscolo

        //        }

        //        EnString = stringBuilder.ToString();

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return EnString;

        //}

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
                Console.WriteLine(ex.Message);
            }

            return new KeyValuePair<string, string>(encryptedResult, encryptedSalt);
        }


    }
}
