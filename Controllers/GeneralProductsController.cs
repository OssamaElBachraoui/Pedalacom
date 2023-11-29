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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GeneralProduct>>> GetProducts()
    {
        if (_context == null)
        {
            return NotFound();
        }

        var products = await _context.GeneralProducts
                .FromSqlRaw("SELECT * FROM View_prodotti")
            .OrderBy(ob => ob.ProductID)
            .ToListAsync();

        if (products == null || products.Count == 0)
        {
            return NoContent(); 
        }

        return Ok(products); 
    }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralProduct>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.GeneralProducts
                .FromSqlRaw("SELECT * FROM View_prodotti")
                .Where(prod => prod.ProductID == id)
                .FirstOrDefaultAsync(prod => prod.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

    }

}
