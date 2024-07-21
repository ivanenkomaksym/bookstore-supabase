using WebUI.Models;

namespace WebUI.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCart(string userId);

        Task CreateShoppingCart(ShoppingCart shoppingCart);
        Task<bool> UpdateShoppingCart(ShoppingCart shoppingCart);
        Task<bool> DeleteShoppingCart(string userId);

        Task<ShoppingCart> AddProductToCart(string userId, StockItem stockItem, ushort quantity = 1);

        Task<bool> RemoveProductFromShoppingCart(string userId, int productId);

        Task<bool> Checkout(string userId);
    }
}
