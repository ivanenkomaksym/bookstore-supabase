using WebUI.Models;

namespace WebUI.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDatabaseService _client;
        private readonly ILogger<ProductService> _logger;

        public ShoppingCartService(IDatabaseService client, ILogger<ProductService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ShoppingCart> AddProductToCart(string userId, StockItem stockItem, ushort quantity = 1)
        {
            var cart = await GetShoppingCart(userId);

            var items = cart.CartItems.Where(x => x.StockItem.Id == stockItem.Id).ToList();
            if (items.Any())
            {
                // Update existing ShoppingCartItem
                items.First().Quantity += quantity;
            }
            else
            {
                // Create new ShoppingCartItem
                cart.CartItems.Add(new ShoppingCartItem
                {
                    StockItem = stockItem,
                    Quantity = quantity
                });
            }

            await _client.Update<ShoppingCart>(cart);

            return cart;
        }

        public async Task<bool> RemoveProductFromShoppingCart(string userId, int productId)
        {
            var cart = await GetShoppingCart(userId);

            // Remove ShoppingCartItem
            var item = cart.CartItems.Single(x => x.StockItem.Product.Id == productId);
            cart.CartItems.Remove(item);

            await _client.Delete<ShoppingCartItem>(x => x.Id == item.Id);
            await _client.Update<ShoppingCart>(cart);

            return true;
        }

        public Task<bool> Checkout(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateShoppingCart(ShoppingCart shoppingCart)
        {
            await _client.Insert<ShoppingCart>(shoppingCart);
        }

        public Task<bool> DeleteShoppingCart(string userId)
        {
            return _client.DeleteFilter<ShoppingCart>(shoppingCart => shoppingCart.UserId == userId);
        }

        public async Task<ShoppingCart> GetShoppingCart(string userId)
        {
            var cart = await _client.FromFilter<ShoppingCart>(cart => cart.UserId == userId);
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                await _client.Insert(cart);
            }

            return cart;
        }

        //private async Task<ShoppingCartItem> GetShoppingCartItem(int shoppingCartItemId)
        //{
        //    var shoppingCartItem = await _client.FromFilter<ShoppingCartItem>(cartItem => cartItem.Id == shoppingCartItemId);
        //    var product = await _client.FromFilter<Product>(product => product.Id == shoppingCartItem.ProductId);
        //    var stockItem = await _client.FromFilter<StockItem>(stockItem => stockItem.ProductId == shoppingCartItem.ProductId);

        //    shoppingCartItem.ProductName = product.Name;
        //    shoppingCartItem.ProductPrice = product.Price;
        //    shoppingCartItem.ImageFile = product.ImageFile;
        //    shoppingCartItem.AvailableOnStock = stockItem.AvailableOnStock;

        //    return shoppingCartItem;
        //}

        public async Task<bool> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            var upsertResult = await _client.Update<ShoppingCart>(shoppingCart);
            return upsertResult.Count > 0;
        }
    }
}
