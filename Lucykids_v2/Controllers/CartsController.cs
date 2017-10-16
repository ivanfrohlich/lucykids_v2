using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lucykids_v2.DAL;
using Lucykids_v2.Models;
using Lucykids_v2.Models.ViewModels;

namespace Lucykids_v2.Controllers
{
    public class CartsController : Controller
    {
        private readonly Cart _cart;
        private readonly StoreDbContext _storeDbContext;

        public CartsController(StoreDbContext storeDbContext, Cart cart)
        {
            _storeDbContext = storeDbContext;
            _cart = cart;
        }

        public ViewResult Index()
        {
            var lines = _cart.GetCartLines();
            _cart.CartLines = lines;

            var cartViewModel = new CartViewModel
            {
                Cart = _cart,
                CartTotal = _cart.GetCartTotal()
            };

            return View(cartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            var selectedProduct = _storeDbContext.Products
                .Include(p => p.ProductImageMappings)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _cart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _storeDbContext.Products.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _cart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}
