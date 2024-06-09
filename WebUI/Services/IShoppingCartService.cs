using WebUI.Models;

namespace WebUI.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCart(string userId);

        Task CreateShoppingCart(ShoppingCart shoppingCart);
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart);
        Task<bool> DeleteShoppingCart(string userId);

        Task<ShoppingCart> AddProductToCart(string userId, ProductWithStock product, ushort quantity = 1);

        Task<bool> Checkout(string userId);
    }
}
