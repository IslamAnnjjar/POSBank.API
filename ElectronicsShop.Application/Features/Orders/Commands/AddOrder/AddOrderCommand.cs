using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using ElectronicsShop.Application.Common.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ElectronicsShop.Domain.Entities;
using ElectronicsShop.Application.Features.Products.Commands.AddProduct;

namespace ElectronicsShop.Application.Features.Categories.Commands.AddCategory;

public class AddOrderCommand : IRequest<int>,
    IMapFrom<Order>
{
    public string CustomerName { get; set; } = null!;
    public string CustomerPhone { get; set; } = null!;
    public decimal Total { get; set; }
    public DateTime Date { get; set; }

    public List<AddOrderProduct> OrderProducts { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddOrderCommand, Order>();
    }
}

public class AddOrderProduct : IMapFrom<OrderProduct>
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddOrderProduct, OrderProduct>();
    }
}

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddOrderCommandHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Order>(command);

        await _dbContext.Orders.AddAsync(entity, cancellationToken);
        if (await _dbContext.SaveChangesAsync(cancellationToken) <= 0)
        {
            throw new ApplicationException("Failed to add order!");
        }

        return entity.Id;
    }
}