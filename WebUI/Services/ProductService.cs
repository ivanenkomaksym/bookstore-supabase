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

        public async Task<IEnumerable<ProductWithStock>> GetProducts()
        {
            var products = await _client.From<Product>();
            var productsWithStock = await AddStockInformation(products);

            return productsWithStock;
        }

        private async Task<IEnumerable<ProductWithStock>> AddStockInformation(IReadOnlyList<Product> products)
        {
            var productsWithStock = new List<ProductWithStock>();

            foreach (var product in products)
            {
                var productWithStock = await AddStockInformation(product);

                productsWithStock.Add(productWithStock);
            }

            return productsWithStock;
        }

        private async Task<ProductWithStock> AddStockInformation(Product product)
        {
            var stockItem = await _client.FromFilter<StockItem>(stockItem => stockItem.ProductId == product.Id);
            var productWithStock = new ProductWithStock
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Summary = product.Summary,
                ImageFile = product.ImageFile,
                Price = product.Price,
                Quantity = stockItem?.Quantity ?? 0,
                Discount = stockItem?.Discount ?? 0,
                Sold = stockItem?.Sold ?? 0,
                AvailableOnStock = stockItem?.AvailableOnStock ?? 0
            };

            return productWithStock;
        }

        public Task<ProductWithStock> GetProduct(Guid productId)
        {
            return Task.FromResult(new ProductWithStock { });
        }

        public Task<IEnumerable<ProductWithStock>> GetProductsByCategory(string category)
        {
            IEnumerable<ProductWithStock> products = new List<ProductWithStock>();
            return Task.FromResult(products);
        }
    }
}
