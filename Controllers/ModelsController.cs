using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public ModelsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Model>>> GetModelsFromCategory(int id)
        {
            if (_context.Models == null)
            {
                return NotFound();
            }

            var products = await _context.Models
                .FromSqlRaw("SELECT DISTINCT pm.ProductModelID, pm.Name, pc.ProductCategoryID\r\nFROM SalesLT.ProductModel pm\r\nJOIN SalesLT.Product p ON pm.ProductModelID = p.ProductModelID\r\nJOIN SalesLT.ProductCategory pc ON pc.ProductCategoryID = p.ProductCategoryID\r\n")
                .Where(model => model.ProductCategoryID == id)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

    }
}
