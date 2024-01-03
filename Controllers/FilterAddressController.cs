using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilterAddressController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;
        Log log;
        public FilterAddressController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<FilterAddress>>> GetAddressById(int id)
        {
            try
            {
                if (_context.FilterAddresses == null)
                {
                    return NotFound();
                    throw new NotFoundException("Contesto del prodotto non trovato");
                }

                var address = await _context.FilterAddresses
                    .FromSqlRaw("SELECT        SalesLT.CustomerAddress.AddressID, SalesLT.Address.AddressLine1, SalesLT.Address.AddressLine2, SalesLT.Address.City, SalesLT.Address.StateProvince\r\nFROM            SalesLT.CustomerAddress INNER JOIN\r\n                         SalesLT.Address ON SalesLT.CustomerAddress.AddressID = SalesLT.Address.AddressID")
                    .Where(pre => pre.AddressId == id)
                    .ToListAsync();

                if (address == null || !address.Any())
                {
                    return NotFound();
                }

                return Ok(address);
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
