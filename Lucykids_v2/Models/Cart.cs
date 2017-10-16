using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lucykids_v2.DAL;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Lucykids_v2.Models
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; }

        // List of CartLines keeps track of the items in the shopping cart that user wants to buy
        // and is identified by CartId in the CartLines class
        public List<CartLine> CartLines { get; set; }

        private readonly StoreDbContext _storeDbContext;

        private Cart(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        // static method GetCart returns Cart 
        public static Cart GetCart(IServiceProvider services)
        {

            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<StoreDbContext>();

            // if cartId is null then new globally unique identifier is assigned by the system to the string cartId
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Cart(context) { CartId = cartId };
        }

        public void AddToCart(Product product, int amount=1)
        {
            // Poducts are not stored directly to the database, but CartLines are recorded to the database
            var cartLine =
                    _storeDbContext.CartLines.SingleOrDefault(
                        cl => cl.Product.ProductId == product.ProductId && cl.CartId == CartId);
            
            if (cartLine == null)
            {
                cartLine = new CartLine
                {
                    CartId = CartId,
                    Product = product,
                    Quantity = 1
                };

                _storeDbContext.CartLines.Add(cartLine);
            }
            
            _storeDbContext.SaveChanges();
        }

        public void RemoveFromCart(Product product)
        {
            var cartLine =
                    _storeDbContext.CartLines.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.CartId == CartId);

            if (cartLine != null)
            {
                    _storeDbContext.CartLines.Remove(cartLine);
            }
            _storeDbContext.SaveChanges();
        }

        public List<CartLine> GetCartLines()
        {
            return CartLines ??
                   (CartLines =
                       _storeDbContext.CartLines.Where(c => c.CartId == CartId)
                           .Include(s =>s.Product)
                           .Include(s => s.ProductImageMappings)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartLines = _storeDbContext
                .CartLines
                .Where(cart => cart.CartId == CartId);

            _storeDbContext.CartLines.RemoveRange(cartLines);

            _storeDbContext.SaveChanges();
        }



        public decimal GetCartTotal()
        {
            var total = _storeDbContext.CartLines.Where(c => c.CartId == CartId)
                .Select(c => c.Product.Price * c.Quantity).Sum();
            return total;
        }
    }
}

//private List<CartLine> lineCollection = new List<CartLine>();

//public virtual void AddItem(Product product, int quantity)
//{
//    CartLine line = lineCollection
//        .Where(p => p.Product.ProductId == product.ProductId)
//        .FirstOrDefault();

//    if (line == null)
//    {
//        lineCollection.Add(new CartLine
//        {
//            Product = product,
//            Quantity = quantity
//        });
//    }
//    else
//    {
//        line.Quantity += quantity;
//    }
//}

//public virtual void RemoveLine(Product product) =>
//    lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);

//public virtual decimal ComputeTotalValue() =>
//    lineCollection.Sum(e => e.Product.Price * e.Quantity);

//public virtual void Clear() => lineCollection.Clear();


//public virtual IEnumerable<CartLine> Lines => lineCollection;