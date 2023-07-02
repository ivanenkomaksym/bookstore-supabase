using Postgrest.Attributes;
using Postgrest.Models;

namespace WebUI.Models
{
    [Table("StockItems")]
    public class StockItem : BaseModel
    {
        [PrimaryKey("_id", false)]
        public string Id { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("ProductName")]
        public string? ProductName { get; set; }

        [Column("Supplier")]
        public string? Supplier { get; set; }

        [Column("Quantity")]
        public ulong Quantity { get; set; }

        [Column("Price")]
        public double Price { get; set; }

        [Column("Discount")]
        public double Discount { get; set; }

        [Column("Sold")]
        public ulong Sold { get; set; }

        [Column("AvailableOnStock")]
        public ulong AvailableOnStock { get; set; }
    }
}
