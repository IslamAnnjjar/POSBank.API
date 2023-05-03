using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using ElectronicsShop.Application.Common.Exceptions;
using ElectronicsShop.Application.Features.Products.Queries.GetProduct;

namespace ElectronicsShop.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _dbContext;

        public GetAllProductsQueryHandler(
            IMapper mapper,
            IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
            .Include(p => p.ProductOrders)
            .AsNoTracking()
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            if (product is null)
            {
                throw new NotFoundException("Product is not found!");
            }

            return product;
        }

        
    }
}