using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using ElectronicsShop.Application.Common.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ElectronicsShop.Domain.Entities;

namespace ElectronicsShop.Application.Features.Products.Commands.AddProduct;

public class AddProductCommand : IRequest<int>,
    IMapFrom<Product>
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Picture { get; set; }
    public List<AddProductSize> ProductSizes { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddProductCommand, Product>();
    }
}

public class AddProductSize : IMapFrom<ProductSize>
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddProductSize, ProductSize>();
    }
}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddProductCommandHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var isProductExists = await _dbContext.Products.AsNoTracking()
            .AnyAsync(x => x.Name == command.Name, cancellationToken);

        if (isProductExists)
        {
            throw new ApplicationException("Product is exists already!");
        }

        var entity = _mapper.Map<Product>(command);
        await _dbContext.Products.AddAsync(entity, cancellationToken);

        if (await _dbContext.SaveChangesAsync(cancellationToken) <= 0)
        {
            throw new ApplicationException("Failed to add product!");
        }

        return entity.Id;
    }
}