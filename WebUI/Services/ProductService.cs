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

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _client.From<Product>();
        }

        public Task<Product> GetProduct(Guid productId)
        {
            return Task.FromResult(new Product { });
        }

        public Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            IEnumerable<Product> products = new List<Product>();
            return Task.FromResult(products);
        }
    }
}
