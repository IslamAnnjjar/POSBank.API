using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using ElectronicsShop.Application.Common.Exceptions;

namespace ElectronicsShop.Application.Features.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductDto>
{
    public int Id { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetProductQueryHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ProductDto> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.AsNoTracking()
            .Where(c => c.Id == query.Id)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if(product is null)
        {
            throw new NotFoundException("Product is not found!");
        }

        return product;
    }
}