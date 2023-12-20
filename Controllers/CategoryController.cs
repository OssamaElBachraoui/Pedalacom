using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    //[BasicAutorizationAttributes]
    public class CategoryController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;
        Log log;

        public CategoryController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetChildCategory(int id)
        {
            try
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
                    throw new NotFoundException("Categoria non trovata");

                }

                return Ok(Category);
            }
            catch (Exception ex){
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }
    }
}
