using ElectronicsShop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ElectronicsShop.Application.Common.Exceptions;

namespace ElectronicsShop.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteProductCommandHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException("Product is not found!");
        }

        _dbContext.Products.Remove(product);

        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}