using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {

        private readonly AdventureWorksLt2019Context _context;
        Log log;
        public ProductImagesController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductImage>>> GetProductsImages(int id)
        {
            try
            {
                if (_context.ProductImages == null)
                {
                    return NotFound();
                    throw new NotFoundException("Contesto del prodotto non trovato");
                }


                var products = await _context.ProductImages
                    .FromSqlRaw("Select ProductID, ThumbNailPhoto, ThumbnailPhotoFileName from[SalesLT].[Product]")
                    .Where(p => p.ProductId == id)
                    .ToListAsync();

     

                if (products == null || !products.Any())
                {
                    return NotFound();
                }

                return Ok(products);
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
