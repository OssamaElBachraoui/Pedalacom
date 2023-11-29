using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public ModelsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralProduct>> GetModelById(int id)
        {
            if (_context.GeneralProducts == null)
            {
                return NotFound();
            }
            var product = await _context.GeneralProducts
                .FromSqlRaw("SELECT * FROM View_prodotti")
                .Where(model => model.ProductModelId== id)
                .FirstOrDefaultAsync(model => model.ProductModelId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
