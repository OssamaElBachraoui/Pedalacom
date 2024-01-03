using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class CustomerOrder
    {
        [Key]
        public int CustomerId  {get;set;}
        public int SalesOrderId { get;set;}
        public DateTime? ShipDate { get;set;}
        public string SalesOrderNumber { get; set; } = null!;
        public decimal TotalDue { get;set;}
        public int SalesOrderDetailId { get;set;}
        public short OrderQty { get;set;}
        public int ProductId { get;set;}
        public string Name { get; set; } = null!;
        public string? Color { get; set; }


    }
}
