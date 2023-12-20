using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OldCustomersController : ControllerBase
    {

        private readonly AdventureWorksLt2019Context _context;
        Log log;

        public OldCustomersController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<OldCustomer>> GetCustomer(string email)
        {
            try
            {
                var lastCustomer = await _context.OldCustomers
                    .FromSqlRaw("select * from [dbo].[View_IsOld]")
                    .Where(c => c.EmailAddress == email)
                    .OrderByDescending(c => c.CustomerId)
                    .FirstOrDefaultAsync();

                if (lastCustomer == null)
                {
                    return NotFound();
                }

                return lastCustomer;
            }catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }



    }
}
