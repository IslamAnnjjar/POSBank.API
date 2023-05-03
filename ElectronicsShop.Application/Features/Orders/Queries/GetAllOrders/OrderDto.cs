using ElectronicsShop.Application.Common.Mappers;
using AutoMapper;
using ElectronicsShop.Domain.Entities;
using ElectronicsShop.Application.Features.Categories.Commands.AddCategory;

namespace ElectronicsShop.Application.Features.Orders.Queries.GetAllOrders;

public class OrderDto : IMapFrom<Order>
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public DateTime Date { get; set; }

    public List<OrderProductDto> OrderProducts { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>();
    }
}

public class OrderProductDto : IMapFrom<OrderProduct>
{
    public Product Product { get; set; } = null!;
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderProduct, OrderProductDto>();
    }
}