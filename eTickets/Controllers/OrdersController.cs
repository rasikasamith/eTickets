using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var responce = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal= _shoppingCart.GetShoppingCartTotal()
            };
            return View(responce);
        }

        public async Task<RedirectToActionResult> AddItemToShoppigCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if(item!=null)
            {
                _shoppingCart.AddItemtoCart(item);

            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveItemFromShoppigCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);

            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
