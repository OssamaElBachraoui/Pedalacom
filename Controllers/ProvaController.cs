using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
[Route("[controller]")]
public class ProvaController : ControllerBase
{
    private readonly AdventureWorksLt2019Context _context;

    public ProvaController(AdventureWorksLt2019Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductProva>>> GetProductProva()
    {
        if (_context == null)
        {
            return NotFound();
        }

        var products = await _context.Products
                .FromSqlRaw("SELECT * FROM ViewProdotti")
            .OrderBy(ob => ob.ProductId)
            .ToListAsync();

        if (products == null || products.Count == 0)
        {
            return NoContent(); 
        }

        return Ok(products); 
    }
}

}
