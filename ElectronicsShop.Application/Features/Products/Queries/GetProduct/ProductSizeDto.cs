using ElectronicsShop.Application.Common.Mappers;
using AutoMapper;
using ElectronicsShop.Domain.Entities;

namespace ElectronicsShop.Application.Features.Products.Queries.GetProduct;

public class ProductSizeDto : IMapFrom<ProductSize>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSize, ProductSizeDto>();
    }
}