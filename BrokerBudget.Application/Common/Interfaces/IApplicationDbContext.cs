using GameStore.Domain.Entities;
using GameStore.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
