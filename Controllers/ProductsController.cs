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
    public class ProductsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public ProductsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/Products
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products
                .Include(prod => prod.SalesOrderDetails)
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products
                .Include(prod => prod.SalesOrderDetails)
                .FirstOrDefaultAsync(prod => prod.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [BasicAutorizationAttributes]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {

             var foundProduct = await _context.Products
            .Where(p => p.ProductId == id)
            .FirstOrDefaultAsync();


            if (foundProduct == null)
            {
                return NotFound();
            }

            foundProduct.Name = product.Name;
            foundProduct.ProductNumber = product.ProductNumber;
            foundProduct.Color = product.Color;
            foundProduct.StandardCost = product.StandardCost;
            foundProduct.ListPrice = product.ListPrice;
            foundProduct.Size = product.Size;
            foundProduct.Weight = product.Weight;
            foundProduct.ProductCategoryId = product.ProductCategoryId;
            foundProduct.ProductModelId = product.ProductModelId;
            foundProduct.SellStartDate = product.SellStartDate;
            foundProduct.SellEndDate = product.SellEndDate;
            foundProduct.DiscontinuedDate = product.DiscontinuedDate;
            foundProduct.ThumbNailPhoto = product.ThumbNailPhoto;
            foundProduct.ThumbnailPhotoFileName = product.ThumbnailPhotoFileName;
          

            _context.Entry(foundProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [BasicAutorizationAttributes]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.Products'  is null.");
          }
            //rowguid


            Guid nuovoGuid = Guid.NewGuid();
            product.Rowguid = nuovoGuid;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [BasicAutorizationAttributes]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
