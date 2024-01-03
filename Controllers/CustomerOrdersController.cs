﻿using Microsoft.AspNetCore.Mvc;
using Pedalacom.Models;
using Pedalacom.Servizi.Eccezioni;
using Pedalacom.Servizi.Log;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrdersController : ControllerBase
    {


            private readonly AdventureWorksLt2019Context _context;
            Log log;
            public CustomerOrdersController(AdventureWorksLt2019Context context)
            {
                _context = context;
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
                        .FromSqlRaw("select cat.ProductCategoryID, prod.ProductID, prod.Name as product,\r\n\t   prod.ListPrice \t\r\nfrom SalesLT.ProductCategory as cat\r\njoin SalesLT.Product as prod on cat.ProductCategoryID = prod.ProductCategoryID")
                        .Where(pre => pre.ProductCategoryID == id)
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
