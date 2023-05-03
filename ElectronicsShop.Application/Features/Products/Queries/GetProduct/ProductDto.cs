using ElectronicsShop.Application.Common.Mappers;
using AutoMapper;
using ElectronicsShop.Domain.Entities;

namespace ElectronicsShop.Application.Features.Products.Queries.GetProduct;

public class ProductDto : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public List<ProductSizeDto> ProductSizes { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>();
    }
}