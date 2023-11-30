using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreviewProductsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public PreviewProductsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<PreviewProduct>>> GetProductsFromCategory(int id)
        {
            if (_context.PreviewProducts == null)
            {
                return NotFound();
            }

            var products = await _context.PreviewProducts
                .FromSqlRaw("select cat.ProductCategoryID, prod.ProductID, prod.Name as product,\r\n\t   prod.ListPrice \t\r\nfrom SalesLT.ProductCategory as cat\r\njoin SalesLT.Product as prod on cat.ProductCategoryID = prod.ProductCategoryID")
                .Where(pre => pre.ProductCategoryID == id)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

    }
}
