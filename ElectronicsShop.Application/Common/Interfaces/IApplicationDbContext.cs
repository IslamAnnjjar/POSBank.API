using Microsoft.EntityFrameworkCore;
using ElectronicsShop.Domain.Entities;

namespace ElectronicsShop.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}