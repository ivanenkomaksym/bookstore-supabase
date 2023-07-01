using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IProductService productService, ILogger<IndexModel> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _productService.GetProducts();

            return Page();
        }
    }
}