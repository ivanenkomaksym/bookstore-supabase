using Postgrest.Attributes;
using Postgrest.Models;

namespace WebUI.Models
{
    [Table("Products")]
    public class Product : BaseModel
    {
        [PrimaryKey("_id", false)]
        public int Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Category")]
        public string? Category { get; set; }

        [Column("Summary")]
        public string? Summary { get; set; }

        [Column("ImageFile")]
        public string? ImageFile { get; set; }

        [Column("Price")]
        public double Price { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public double DiscountedPrice
        {
            get
            {
                return Price - Price * Discount;
            }
            init { }
        }

        public double Rating
        {
            get
            {
                var rand = new Random();
                return 5.0 - rand.NextDouble();
            }
            init { }
        }

        // StockItem properties
        public ushort Quantity { get; set; }
        public double Discount { get; set; }
        public ushort Sold { get; set; }
        public ushort AvailableOnStock { get; set; }
    }
}
