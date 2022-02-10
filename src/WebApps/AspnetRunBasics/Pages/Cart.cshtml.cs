using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "swn";
            Cart = await _basketService.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            string userName = "swn";
            BasketModel basket = await _basketService.GetBasket(userName);

            //basket.Items.RemoveRange(0,1);
            BasketItemModel item = basket.Items.Single(x => x.ProductId == productId);
            basket.Items.Remove(item);

            BasketModel basketUpdated = await _basketService.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}