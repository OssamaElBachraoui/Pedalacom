using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Pedalacom.BLogic.Encryption
{
    public class DecryptSalt
    {

        internal bool DecryptSaltCredential(Models.Customer customerFound, string password)
        {
            bool result = false;
            byte[] byteSalt = new byte[16];
            string encryptedResult = string.Empty;
            string encryptedSalt = string.Empty;
         
            string pwdHash = string.Empty;

            try
            {
               

                byteSalt = Convert.FromBase64String(customerFound.PasswordSalt);

                encryptedResult = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                        password: password,
                        salt: byteSalt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 16)
                        );

                encryptedSalt = Convert.ToBase64String(byteSalt);

                //7QinOd6EXaMJU + C9FYAPyw ==

                if (customerFound != null)
                {
                    result = customerFound.PasswordHash == encryptedResult;

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
