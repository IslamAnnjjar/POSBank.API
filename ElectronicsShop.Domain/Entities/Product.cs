using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShop.Domain.Entities;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [MaxLength(250)]
    public string Name { get; set; } = null!;
    [MaxLength(250)]
    public string Picture { get; set; }
    
    public decimal Price { get; set; }

    public ICollection<OrderProduct> ProductOrders { get; set; } = null!;
    public ICollection<ProductSize> ProductSizes { get; set; } = null!;
}