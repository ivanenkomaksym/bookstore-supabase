using WebUI.Models;

namespace WebUI.Services
{
    public class ProductService : IProductService
    {
        private readonly IDatabaseService _client;

        public ProductService(IDatabaseService client, ILogger<ProductService> logger)
        {
            _client = client;
        }

        public async Task<IEnumerable<StockItem>> GetProducts()
        {
            var productsWithStock = await _client.From<StockItem>();

            return productsWithStock;
        }

        public async Task<StockItem> GetProduct(int productId)
        {
            var productsWithStock = await _client.FromFilter<StockItem>(p => p.Product.Id == productId);

            return productsWithStock;
        }

        public Task<IEnumerable<StockItem>> GetProductsByCategory(string category)
        {
            IEnumerable<StockItem> products = new List<StockItem>();
            return Task.FromResult(products);
        }
    }
}
