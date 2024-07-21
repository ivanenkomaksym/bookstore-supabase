using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAuthService _authService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IProductService productService, IShoppingCartService shoppingCartService, IAuthService authService, ILogger<IndexModel> logger)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _authService = authService;
            _logger = logger;
        }

        public IEnumerable<StockItem> ProductList { get; set; } = new List<StockItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _productService.GetProducts();

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await _productService.GetProduct(productId);

            var user = await _authService.GetUser();

            var cart = await _shoppingCartService.AddProductToCart(user.Id, product);

            return RedirectToPage("Cart");
        }
    }
}