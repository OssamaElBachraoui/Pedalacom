using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreviewProductsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;
        Log log;
        public PreviewProductsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: PreviewProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreviewProduct>>> GetPreviewProducts()
        {
            if (_context.PreviewProducts == null)
            {
                return NotFound();
            }
            return await _context.PreviewProducts
                .FromSqlRaw("select cat.ProductCategoryID, prod.ProductID, prod.Name as product , prod.ListPrice from SalesLT.ProductCategory as cat join SalesLT.Product as prod on cat.ProductCategoryID = prod.ProductCategoryID")
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<PreviewProduct>>> GetProductsFromCategory(int id)
        {
            try
            {
                if (_context.PreviewProducts == null)
                {
                    return NotFound();
                    throw new NotFoundException("Contesto del prodotto non trovato");
                }


                var products = await _context.PreviewProducts
                    .FromSqlRaw("select cat.ProductCategoryID, prod.ProductID, prod.Name as product , prod.ListPrice from SalesLT.ProductCategory as cat join SalesLT.Product as prod on cat.ProductCategoryID = prod.ProductCategoryID")
                    .Where(pre => pre.ProductCategoryID == id)
                    .ToListAsync();


                if (products == null || !products.Any())
                {
                    return NotFound();
                }

                return Ok(products);
            }catch (Exception ex)
            {
                log = new Log(typeof(Program).ToString(), ex.Message, ex.GetType().ToString(), ex.HResult.ToString(), DateTime.Now);
                log.WriteLog();
                return BadRequest(ex);
            }
        }

    }
}
