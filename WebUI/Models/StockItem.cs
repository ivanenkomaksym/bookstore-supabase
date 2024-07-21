using System.Runtime.Serialization;
using Postgrest.Attributes;
using Postgrest.Models;

namespace WebUI.Models
{
    [Table("StockItems")]
    public class StockItem : BaseModel
    {
        [PrimaryKey("_id", false)]
        public int Id { get; set; }

        [Reference(typeof(Product))]
        public Product Product { get; set; }

        [Column("Supplier")]
        public string? Supplier { get; set; }

        [Column("Quantity")]
        public ulong Quantity { get; set; }

        [Column("Discount")]
        public double Discount { get; set; }

        [Column("Sold")]
        public ulong Sold { get; set; }

        [Column("AvailableOnStock")]
        public ulong AvailableOnStock { get; set; }

        [IgnoreDataMember]
        public double DiscountedPrice
        {
            get
            {
                return Product.Price - Product.Price * Discount;
            }
            init { }
        }
    }
}
