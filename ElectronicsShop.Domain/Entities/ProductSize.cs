using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShop.Domain.Entities;

[Table("ProductSizes")]
public class ProductSize
{
    [Key]
    public int Id { get; set; }

    [MaxLength(250)]
    public string Name { get; set; }
    public decimal Price { get; set; }
}