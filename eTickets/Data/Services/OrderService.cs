using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class OrderService : IOrdersService
    {
        private readonly AppDbContext _appDbContext;
        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
        {
            var orders =await _appDbContext.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Where(n => n.UserId == userId).ToListAsync();

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    Price = item.Movie.Price,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id //This is the order Id that was saved recently.
                };
                await _appDbContext.OrderItems.AddAsync(orderItem);
            }
            await _appDbContext.SaveChangesAsync();
        }
    }
}
