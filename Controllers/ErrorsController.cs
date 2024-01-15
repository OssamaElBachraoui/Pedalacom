using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public ErrorsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/Errors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Errori>>> GetError()
        {
          if (_context.Errori == null)
          {
              return NotFound();
          }
            return await _context.Errori.ToListAsync();
        }

        // GET: api/Errors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Errori>> GetError(int id)
        {
          if (_context.Errori == null)
          {
              return NotFound();
          }
            var error = await _context.Errori.FindAsync(id);

            if (error == null)
            {
                return NotFound();
            }

            return error;
        }

        // PUT: api/Errors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutError(int id, Errori error)
        {
            if (id != error.IdErrore)
            {
                return BadRequest();
            }

            _context.Entry(error).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorExists(id))
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

        // POST: api/Errors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Errori>> PostError(Errori error)
        {
          if (_context.Errori == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.Error'  is null.");
          }
            _context.Errori.Add(error);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetError", new { id = error.IdErrore }, error);
        }

        // DELETE: api/Errors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteError(int id)
        {
            if (_context.Errori == null)
            {
                return NotFound();
            }
            var error = await _context.Errori.FindAsync(id);
            if (error == null)
            {
                return NotFound();
            }

            _context.Errori.Remove(error);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrorExists(int id)
        {
            return (_context.Errori?.Any(e => e.IdErrore == id)).GetValueOrDefault();
        }
    }
}
