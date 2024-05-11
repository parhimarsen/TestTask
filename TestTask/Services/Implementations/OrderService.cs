using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
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
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            return order;
        }

        public Task<List<Order>> GetOrders()
        {
            var activeUserIds = _context.Users
                .Where(u => u.Status == UserStatus.Active)
                .Select(u => u.Id);

            var orders = _context.Orders
                .Where(o => activeUserIds.Contains(o.Id))
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();

            return orders;
        }
    }
}
