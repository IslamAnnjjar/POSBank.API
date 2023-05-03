using ElectronicsShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicsShop.Infrastructure.ApplicationContext;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(op => new { op.OrderId, op.ProductId })
            .HasName("PrimaryKey_OrderProductId");

        builder.HasOne<Order>(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        builder.HasOne<Product>(op => op.Product)
            .WithMany(o => o.ProductOrders)
            .HasForeignKey(op => op.ProductId);
    }
}