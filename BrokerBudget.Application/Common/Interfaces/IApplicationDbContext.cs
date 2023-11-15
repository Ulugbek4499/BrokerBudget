using BrokerBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrokerBudget.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGiver> ProductGivers { get; set; }
        public DbSet<ProductTaker> ProductTakers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
