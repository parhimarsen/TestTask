using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var user = await _context.Users
                .Select(u => new
                {
                    User = u,
                    CommonPrice = u.Orders.Where(o => o.CreatedAt.Year == 2003).Sum(o => o.Quantity * o.Price)
                })
                .OrderByDescending(x => x.CommonPrice)
                .FirstOrDefaultAsync();

            return user.User;
        }

        public Task<List<User>> GetUsers()
        {
            var users = _context.Users.Where(
                u => u.Orders.Any(o => o.Status == OrderStatus.Paid && o.CreatedAt.Year == 2010)
            )
            .ToListAsync();

            return users;

        }
    }
}
