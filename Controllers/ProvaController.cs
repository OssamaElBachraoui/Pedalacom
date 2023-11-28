using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedalacom.Models;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvaController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public ProvaController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductProva>>> getProductProva()
        {
            if(_context == null)
            {
                return NotFound();
            }
            return await _context.Products
          .FromSqlRaw($"select p.ProductID, p.Name, p.Color, p.ListPrice, p.Size, p.Weight, pc.ProductCategoryID, pc.ParentProductCategoryID, pc.Name, pm.Name, pd.Description\r\nfrom SalesLT.Product p join SalesLT.ProductCategory pc on p.ProductCategoryID=pc.ProductCategoryID\r\njoin SalesLT.ProductModel pm on p.ProductModelID=pm.ProductModelID \r\njoin SalesLT.ProductModelProductDescription pmpd on p.ProductModelID=pmpd.ProductModelID\r\njoin SalesLT.ProductDescription pd on pmpd.ProductDescriptionID=pd.ProductDescriptionID")
          .OrderBy(ob => ob.ProductId)
          .ToListAsync();

            // Passare i dati al
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        //{
        //    if (_context.Customers == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Customers
        //        .Include(emp => emp.CustomerAddresses)
        //        .Include(emp => emp.SalesOrderHeaders)
        //        .ToListAsync();
        //}

    //        var page = await _context.ReducedCustomers.FromSql(
    //$"SELECT CustomerId,FirstName,LastName,CompanyName,EmailAddress FROM [SalesLT].[Customer]")
    //.OrderBy(ob => ob.CustomerId)
    //.Where(c => c.CustomerId > dataPosition)
    //.ToListAsync();
    //    var x = DbSocieta.Database.SqlQuery<Impiegato>($"sp_scaffolding {id}");
    }
}
