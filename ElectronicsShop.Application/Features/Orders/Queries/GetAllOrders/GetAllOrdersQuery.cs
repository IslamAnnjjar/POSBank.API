using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace ElectronicsShop.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<List<OrderDto>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetAllCategoriesQueryHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var categories = await _dbContext.Orders
            .Include(x => x.OrderProducts)
            .AsNoTracking()
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return categories;
    }
}