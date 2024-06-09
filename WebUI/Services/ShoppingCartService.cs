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

        public async Task<ShoppingCart> AddProductToCart(string userId, ProductWithStock product, ushort quantity = 1)
        {
            var cart = await GetShoppingCart(userId);

            var items = cart.CartItems.Where(x => x.ProductId == product.Id).ToList();
            if (items.Any())
            {
                items.First().Quantity += quantity;
            }
            else
            {
                var item = new ShoppingCartItem
                {
                    ProductId = product.Id,
                    Quantity = quantity
                };

                cart.CartItems.Add(item);
            }

            await UpdateShoppingCart(cart);

            return cart;
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

            var shoppingCartItems = await AddItemsToCart(cart);
            cart.CartItems = shoppingCartItems.ToList();

            return cart;
        }

        private async Task<IEnumerable<ShoppingCartItem>> AddItemsToCart(ShoppingCart cart)
        {
            var shoppingCartItems = new List<ShoppingCartItem>();

            foreach (var item in cart.DbItems)
            {
                var shoppingCartItem = await GetShoppingCartItem(item);

                shoppingCartItems.Add(shoppingCartItem);
            }

            return shoppingCartItems;
        }

        private async Task<ShoppingCartItem> GetShoppingCartItem(int shoppingCartItemId)
        {
            var shoppingCartItem = await _client.FromFilter<ShoppingCartItem>(cartItem => cartItem.Id == shoppingCartItemId);
            var product = await _client.FromFilter<Product>(product => product.Id == shoppingCartItem.ProductId);
            var stockItem = await _client.FromFilter<StockItem>(stockItem => stockItem.ProductId == shoppingCartItem.ProductId);

            shoppingCartItem.ProductName = product.Name;
            shoppingCartItem.ProductPrice = product.Price;
            shoppingCartItem.ImageFile = product.ImageFile;
            shoppingCartItem.AvailableOnStock = stockItem.AvailableOnStock;

            return shoppingCartItem;
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            var insertedCartItems = new List<ShoppingCartItem>{ };
            foreach (var item in shoppingCart.CartItems)
            {
                insertedCartItems = await _client.Insert<ShoppingCartItem>(item);
                if (insertedCartItems == null)
                    throw new Exception($"Failed to insert {nameof(ShoppingCartItem)}");
            }

            shoppingCart.DbItems = insertedCartItems.Select(item => item.Id).ToList();

            var models = await _client.Update<ShoppingCart>(shoppingCart);
            return models.Where(cart => cart.Id == shoppingCart.Id).FirstOrDefault();
        }
    }
}
