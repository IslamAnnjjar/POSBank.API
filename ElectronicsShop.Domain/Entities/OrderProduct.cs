using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShop.Domain.Entities;

[Table("OrderProducts")]
public class OrderProduct
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public decimal Price { get; set; }
}