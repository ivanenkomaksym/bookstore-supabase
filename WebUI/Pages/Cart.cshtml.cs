using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Pages
{
    public class CartModel : PageModel
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAuthService _authService;

        public CartModel(IShoppingCartService shoppingCartService, IAuthService authService)
        {
            _shoppingCartService = shoppingCartService;
            _authService = authService;
        }

        [BindProperty]
        public ShoppingCart Cart { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _authService.GetUser();

            Cart = await _shoppingCartService.GetShoppingCart(user.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostIncreaseQuantity(int itemId, ushort quantity)
        {
            var user = await _authService.GetUser();

            Cart = await _shoppingCartService.GetShoppingCart(user.Id);

            // Find the item in the shopping cart
            var item = Cart.CartItems.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                // Increase the quantity
                item.Quantity = quantity;

                // Update the total price of the shopping cart
                Cart.TotalPrice = CalculateTotalPrice();
            }

            await _shoppingCartService.UpdateShoppingCart(Cart);

            return Page();
        }

        private double CalculateTotalPrice()
        {        
            // Calculate the total price based on the items in the shopping cart
            return Cart.CartItems.Sum(item => item.StockItem.Product.Price * item.Quantity);
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(int productId)
        {
            var user = await _authService.GetUser();

            await _shoppingCartService.RemoveProductFromShoppingCart(user.Id, productId);

            return RedirectToPage();
        }
    }
}
