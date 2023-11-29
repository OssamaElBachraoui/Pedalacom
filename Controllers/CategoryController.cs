using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public CategoryController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetChildCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }

            var Category = await _context.Categories
                .FromSqlRaw("select ParentProductCategoryID,  ProductCategoryID, Name  from SalesLT.ProductCategory")
                .Where(cat => cat.ParentProductCategoryID == id)
            .ToListAsync();



            if (Category == null)
            {
                return NotFound();
            }

            return Ok(Category);
        }
    }
}
