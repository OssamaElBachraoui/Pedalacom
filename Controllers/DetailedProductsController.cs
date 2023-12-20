using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [ApiController]
[Route("[controller]")]
public class DetailedProductsController : ControllerBase
{
    private readonly AdventureWorksLt2019Context _context;
        Log log;

    public DetailedProductsController(AdventureWorksLt2019Context context)
    {
        _context = context;
    }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedProduct>> GetProductById(int id)
        {
            try
            {
                if (_context.DetailedProducts == null)
                {
                    return NotFound();
                    throw new NotFoundException("Contesto del prodotto non trovato");
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
            }catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }



    }

}
