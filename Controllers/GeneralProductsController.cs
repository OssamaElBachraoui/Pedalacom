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
    public async Task<ActionResult<IEnumerable<GeneralProduct>>> GetProductProva()
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
}

}
