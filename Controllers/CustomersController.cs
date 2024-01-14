using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.BLogic.Authentication;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CustomersController : ControllerBase
    {
        Log log;
        private readonly AdventureWorksLt2019Context _context;

        public CustomersController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                if (_context.Customers == null)
                {
                    return NotFound();
                    throw new NotFoundException("Cliente non trovato");
                }
                return await _context.Customers
                    .Include(emp => emp.CustomerAddresses)
                    .Include(emp => emp.SalesOrderHeaders)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            try
            {
                var lastCustomer = await _context.Customers
                    .Where(c => c.EmailAddress == email)
                    .OrderByDescending(c => c.CustomerId)
                    .Include(ad => ad.CustomerAddresses)
                    .Include(or => or.SalesOrderHeaders)
                    .FirstOrDefaultAsync();

                if (lastCustomer == null)
                {
                    return NotFound();
                    throw new NotFoundException("Prodotto non trovato");
                }

                return lastCustomer;
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{email}")]
        public async Task<IActionResult> PutCustomer(string email, Customer customer)
        {

            var lastCustomer = await _context.Customers
             .Where(c => c.EmailAddress == email)
             .OrderByDescending(c => c.CustomerId)
             .FirstOrDefaultAsync();

            if (lastCustomer == null)
            {
                return NotFound();
            }

            if (customer.tmpPassword != null)
            {
                Encryption en = new Encryption();
                KeyValuePair<string, string> keyValuePair;
                keyValuePair = en.EncrypSaltString(customer.tmpPassword);
                customer.PasswordHash = keyValuePair.Key;
                customer.PasswordSalt = keyValuePair.Value;
                customer.tmpPassword = null;
            }

            lastCustomer.FirstName = customer.FirstName;
            lastCustomer.LastName = customer.LastName;
            lastCustomer.Phone = customer.Phone;
            lastCustomer.PasswordHash = customer.PasswordHash;
            lastCustomer.PasswordSalt = customer.PasswordSalt;
            lastCustomer.tmpPassword = customer.tmpPassword;
            lastCustomer.IsOld = 0;

            _context.Entry(lastCustomer).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();


                if (!CustomerExists(lastCustomer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            try
            {
                if (_context.Customers == null)
                {
                    return Problem("Entity set 'AdventureWorksLt2019Context.Customers'  is null.");
                    throw new Exception("Contesto database nullo");
                }

                if (_context.Customers.Any(c => c.EmailAddress == customer.EmailAddress))
                {
                    return BadRequest("Email address is already in use.");
                  
                }
                //password hash
                Encryption en = new Encryption();
                KeyValuePair<string, string> keyValuePair;
                keyValuePair = en.EncrypSaltString(customer.tmpPassword);
                customer.PasswordHash = keyValuePair.Key;
                customer.PasswordSalt = keyValuePair.Value;


                //rowguid
                Guid nuovoGuid = Guid.NewGuid();
                customer.Rowguid = nuovoGuid;

                
                customer.tmpPassword = null;

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);

            }
        }

        // DELETE: api/Customers/5
        [BasicAutorizationAttributes]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (_context.Customers == null)
                {
                    return NotFound();
                    throw new NotFoundException("Contesto del cliente non trovato");
                }
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                return NoContent();
            }catch(Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);

            }
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
