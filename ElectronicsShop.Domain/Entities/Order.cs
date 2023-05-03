using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShop.Domain.Entities;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }
    public decimal Total { get; set; }

    [MaxLength(250)]
    public string CustomerName { get; set; }
    [MaxLength(250)]
    public string CustomerPhone { get; set; }

    public DateTime Date { get; set; }

    public ICollection<OrderProduct> OrderProducts { get; set; } = null!;
}