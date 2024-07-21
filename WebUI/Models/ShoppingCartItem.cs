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

        [Column("Quantity")]
        public ushort Quantity { get; set; }

        [Reference(typeof(StockItem))]
        public StockItem StockItem { get; set; }
    }
}
