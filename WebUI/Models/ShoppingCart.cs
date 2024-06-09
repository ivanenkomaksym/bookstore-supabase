using System.Runtime.Serialization;
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

        [Column("UserEmail")]
        public string UserEmail { get; init; }

        [Column("TotalPrice")]
        public double TotalPrice { get; set; }

        [Column("Items")]
        public List<int> DbItems { get; set; } = new List<int> {};

        [IgnoreDataMember]
        public List<ShoppingCartItem> CartItems { get; set; } = new List<ShoppingCartItem>();
    }
}
