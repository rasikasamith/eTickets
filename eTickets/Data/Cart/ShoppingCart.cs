using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemtoCart(Movie movie)
        {
            var shoppingCartitem = _appDbContext.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if(shoppingCartitem==null)
            {
                shoppingCartitem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                
                _appDbContext.ShoppingCartItems.Add(shoppingCartitem);
            }
            else
            {
                shoppingCartitem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartitem = _appDbContext.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartitem != null)
            {
                if (shoppingCartitem.Amount > 1)
                {
                    shoppingCartitem.Amount--;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartitem);
                }
            }

            _appDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _appDbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }

    }
}
