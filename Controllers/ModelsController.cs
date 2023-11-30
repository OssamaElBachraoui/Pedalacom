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
                .FromSqlRaw("select distinct pm.ProductModelID,pm.Name,pc.ProductCategoryID,p.ListPrice from SalesLT.ProductModel pm join SalesLT.Product p on pm.ProductModelID=p.ProductModelID\r\njoin SalesLT.ProductCategory pc on pc.ProductCategoryID=p.ProductCategoryID")
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
