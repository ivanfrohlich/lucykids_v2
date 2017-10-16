using Lucykids_v2.Models;
using Lucykids_v2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly Cart _cart;

        public ShoppingCartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            var cartLines = _cart.GetCartLines();
            _cart.CartLines = cartLines;

            var cartViewModel = new CartViewModel
            {
                Cart = _cart,
                CartTotal = _cart.GetCartTotal()
            };

            return View(cartViewModel);
        }
    }
}
