using Postgrest.Attributes;
using Postgrest.Models;

namespace WebUI.Models
{
    [Table("ShoppingCarts")]
    public class ShoppingCart : BaseModel
    {
        [PrimaryKey("_id", false)]
        public int Id { get; init; }

        [Column("UserId")]
        public string UserId { get; init; }

        [Column("TotalPrice")]
        public double TotalPrice { get; set; }

        [Reference(typeof(ShoppingCartItem))]
        public List<ShoppingCartItem> CartItems { get; set; } = new();
    }
}
