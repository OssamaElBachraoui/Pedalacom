using Microsoft.AspNetCore.Mvc;
using Pedalacom.Models;
using Microsoft.EntityFrameworkCore;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryParentsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public CategoryParentsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralProduct>> GetCategory()
        {
            if (_context.CategoriesParent == null)
            {
                return NotFound();
            }
            var product = await _context.CategoriesParent
                .FromSqlRaw("select ProductCategoryID, Name from SalesLT.ProductCategory\r\nwhere ParentProductCategoryID  is NULL")
                .ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
