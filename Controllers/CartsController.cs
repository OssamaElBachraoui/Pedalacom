using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.BLogic.Authentication;
using Pedalacom.Models;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [BasicAutorizationAttributes]
    public class CartsController : ControllerBase
    {
        Log log;
        private readonly AdventureWorksLt2019Context _context;

        public CartsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
        {
            try
            {
                if (_context.Cart == null)
                {
                    return NotFound();
                }
                return await _context.Cart.ToListAsync();

            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }

        }

        // GET: api/Carts/5
        [HttpGet("{customerId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart(int customerId)
        {
            try
            {

                if (_context.Cart == null)
                {
                    return NotFound();
                }

                var carts = await _context.Cart
                    .Where(c => c.CustomerID == customerId)
                    .ToListAsync();

                if (carts == null || !carts.Any())
                {
                    return NotFound();
                }

                return carts;
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }

        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            try
            {

                if (_context.Cart == null)
                {
                    return Problem("Entity set 'AdventureWorksLt2019Context.Cart'  is null.");
                }
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCart", new { id = cart.CartID }, cart);
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                if (_context.Cart == null)
                {
                    return NotFound();
                }
                var cart = await _context.Cart.FindAsync(id);
                if (cart == null)
                {
                    return NotFound();
                }

                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }

        private bool CartExists(int id)
        {
            return (_context.Cart?.Any(e => e.CartID == id)).GetValueOrDefault();
        }
    }
}
