using Pedalacom.Models;

namespace Pedalacom.BLogic.ProductNumberGenerator
{
    public class ProductNumber
    {
        public string GenerateRandomProductNumber()
        {
            // Generate a random alphanumeric product number (e.g., BK-M18B-42)
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";

            Random random = new Random();

            // Generate a random product size
            int randomSize = random.Next(10, 100); // Adjust the range as needed

            string productNumber = $"{letters[random.Next(letters.Length)]}K-M{randomSize}{letters[random.Next(letters.Length)]}-{random.Next(100)}";

            return productNumber;
        }

    }
}
