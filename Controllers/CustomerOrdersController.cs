using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrdersController : ControllerBase
    {


            private readonly AdventureWorksLt2019Context _context;
            Log log;
            public CustomerOrdersController(AdventureWorksLt2019Context context)
            {
                _context = context;
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<List<CustomerOrder>>> GetOrders(int id)
            {
                try
                {
                    if (_context.CustomerOrders == null)
                    {
                        return NotFound();
                        throw new NotFoundException("Contesto del prodotto non trovato");
                    }

                    var orders = await _context.CustomerOrders
                        .FromSqlRaw("SELECT * FROM [dbo].[View_Orders]")
                        .Where(pre => pre.CustomerId == id)
                        .ToListAsync();

                    if (orders == null || !orders.Any())
                    {
                        return NotFound();
                    }

                    return Ok(orders);
                }
                catch (Exception ex)
                {
                    log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                    log.WriteLog();
                    return BadRequest(ex);
                }
            }

        
    }
}
