using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.BLogic.Authentication;
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
        [BasicAutorizationAttributes]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Errori>>> GetError()
        {
            var erroriQuery = _context.Errori.OrderByDescending(e => e.DataErrore);

            var ultimiErrori = await erroriQuery.Take(100).ToListAsync();

            if (_context.Errori == null)
          {
              return NotFound();
          }
            return ultimiErrori;
        }


        private bool ErrorExists(int id)
        {
            return (_context.Errori?.Any(e => e.IdErrore == id)).GetValueOrDefault();
        }
    }
}
