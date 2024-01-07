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
    public class CategoryChildsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public CategoryChildsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/CategoryChilds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryChild>>> GetCategoryChild()
        {
          if (_context.CategoryChild == null)
          {
              return NotFound();
          }
            return await _context.CategoryChild
                .FromSqlRaw("SELECT        TOP (100) PERCENT ProductCategoryID, Name\r\nFROM            SalesLT.ProductCategory\r\nWHERE        (ProductCategoryID >= 5)\r\nORDER BY ProductCategoryID")
                .ToListAsync();
        }

        // GET: api/CategoryChilds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryChild>> GetCategoryChild(int id)
        {
          if (_context.CategoryChild == null)
          {
              return NotFound();
          }
            var categoryChild = await _context.CategoryChild.FindAsync(id);

            if (categoryChild == null)
            {
                return NotFound();
            }

            return categoryChild;
        }

        // PUT: api/CategoryChilds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryChild(int id, CategoryChild categoryChild)
        {
            if (id != categoryChild.ProductCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(categoryChild).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryChildExists(id))
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

        // POST: api/CategoryChilds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryChild>> PostCategoryChild(CategoryChild categoryChild)
        {
          if (_context.CategoryChild == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.CategoryChild'  is null.");
          }
            _context.CategoryChild.Add(categoryChild);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryChild", new { id = categoryChild.ProductCategoryID }, categoryChild);
        }

        // DELETE: api/CategoryChilds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryChild(int id)
        {
            if (_context.CategoryChild == null)
            {
                return NotFound();
            }
            var categoryChild = await _context.CategoryChild.FindAsync(id);
            if (categoryChild == null)
            {
                return NotFound();
            }

            _context.CategoryChild.Remove(categoryChild);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryChildExists(int id)
        {
            return (_context.CategoryChild?.Any(e => e.ProductCategoryID == id)).GetValueOrDefault();
        }
    }
}
