using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
[Route("[controller]")]
public class DetailedProductsController : ControllerBase
{
    private readonly AdventureWorksLt2019Context _context;

    public DetailedProductsController(AdventureWorksLt2019Context context)
    {
        _context = context;
    }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedProduct>> GetProductById(int id)
        {
            if (_context.DetailedProducts == null)
            {
                return NotFound();
            }
            var product = await _context.DetailedProducts
                .FromSqlRaw("Select * from [dbo].[View_Prodotti]")
                .Where(prod => prod.ProductID == id)
                .FirstOrDefaultAsync(prod => prod.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }



    }

}
