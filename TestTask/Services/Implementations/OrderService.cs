using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Order> GetOrder()
        {
            var order = _context.Orders
                .OrderByDescending(_ => _.Price * _.Quantity)
                .FirstAsync();
                
            return order;
        }

        public Task<List<Order>> GetOrders()
        {
            var orders = _context.Orders
                .Where(_ => _.Quantity > 10)
                .ToListAsync();

            return orders;
        }
    }
}
