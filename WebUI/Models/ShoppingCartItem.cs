using System.Runtime.Serialization;
using Postgrest.Attributes;
using Postgrest.Models;

namespace WebUI.Models
{
    [Table("ShoppingCartItems")]
    public class ShoppingCartItem : BaseModel
    {
        [PrimaryKey("_id", false)]
        public int Id { get; init; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("Quantity")]
        public ushort Quantity { get; set; }

        // Product properties
        [IgnoreDataMember]
        public string? ProductName { get; set; }

        [IgnoreDataMember]
        public double ProductPrice { get; set; }

        [IgnoreDataMember]
        public string? ImageFile { get; set; }

        [IgnoreDataMember]
        public ulong AvailableOnStock { get; set; }

    }
}
