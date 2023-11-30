using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
[Route("[controller]")]
public class GeneralProductsController : ControllerBase
{
    private readonly AdventureWorksLt2019Context _context;

    public GeneralProductsController(AdventureWorksLt2019Context context)
    {
        _context = context;
    }

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<GeneralProduct>>> GetProducts()
    //{
    //    if (_context == null)
    //    {
    //        return NotFound();
    //    }

    //    var products = await _context.GeneralProducts
    //            .FromSqlRaw("SELECT * FROM View_prodotti")
    //        .OrderBy(ob => ob.ProductID)
    //        .ToListAsync();

    //    if (products == null || products.Count == 0)
    //    {
    //        return NoContent(); 
    //    }

    //    return Ok(products); 
    //}

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralProduct>> GetProductById(int id)
        {
            if (_context.GeneralProducts == null)
            {
                return NotFound();
            }
            var product = await _context.GeneralProducts
                .FromSqlRaw("Select mod.ProductModelID, mod.Name as model, prod.Color, prod.Size, prod.Weight, prod.Name as prodotto, prod.ListPrice\r\nfrom SalesLT.ProductModel as mod \r\njoin SalesLT.Product as prod on mod.ProductModelID = prod.ProductModelID")
                .Where(prod => prod.ProductModelId == id)
                .ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        

    }

}
