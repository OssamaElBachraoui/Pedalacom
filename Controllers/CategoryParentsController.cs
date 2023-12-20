using Microsoft.AspNetCore.Mvc;
using Pedalacom.Models;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryParentsController : ControllerBase
    {
        Log log;
        private readonly AdventureWorksLt2019Context _context;

        public CategoryParentsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<DetailedProduct>> GetCategory()
        {
            try
            {
                if (_context.CategoriesParent == null)
                {
                    return NotFound();
                    throw new NotFoundException("Categoria padre non trovata");
                }
                var product = await _context.CategoriesParent
                    .FromSqlRaw("select ProductCategoryID, Name from SalesLT.ProductCategory\r\nwhere ParentProductCategoryID  is NULL")
                    .ToListAsync();

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
