using ElectronicsShop.Application.Common.Interfaces;
using ElectronicsShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ElectronicsShop.Infrastructure.ApplicationContext;

public class ApplicationDbContext : DbContext,
    IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public virtual DbSet<ProductSize> ProductSizes { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}