using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using ElectronicsShop.Application.Common.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ElectronicsShop.Domain.Entities;
using ElectronicsShop.Application.Common.Exceptions;
using ElectronicsShop.Application.Features.Orders.Queries.GetAllOrders;

namespace ElectronicsShop.Application.Features.Products.Commands.AddProduct;

public class UpdateProductCommand : IRequest<bool>,
    IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public List<UpdateProductSize> ProductSizes { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductCommand, Product>();
    }
}

public class UpdateProductSize : IMapFrom<ProductSize>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductSize, ProductSize>();
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.Include(x => x.ProductSizes)
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException("Product is not found!");
        }

        ModifyEntity(command, product);

        _dbContext.Products.Update(product);

        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    private void ModifyEntity(UpdateProductCommand command, Product entityToBeUpdated)
    {
        entityToBeUpdated.Name = command.Name;
        entityToBeUpdated.Price = command.Price;

        foreach(var productSize in entityToBeUpdated.ProductSizes)
        {
            entityToBeUpdated.ProductSizes.Remove(productSize);
        }

        foreach (var productSize in command.ProductSizes)
        {
            entityToBeUpdated.ProductSizes.Add(
                new ProductSize { 
                    Name = productSize.Name,
                    Price = productSize.Price
                }
            );
        }
    }
}