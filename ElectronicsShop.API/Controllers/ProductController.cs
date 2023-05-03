using ElectronicsShop.API.Models.Response;
using ElectronicsShop.Application.Features.Products.Commands.AddProduct;
using ElectronicsShop.Application.Features.Products.Commands.DeleteProduct;
using ElectronicsShop.Application.Features.Products.Queries.GetAllProducts;
using ElectronicsShop.Application.Features.Products.Queries.GetProduct;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsShop.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet]
    [ActionName("GetAll")]
    public async Task<ActionResult<GetAllOrdersResponse>> GetAll()
    {
        return Ok(await Mediator.Send(new
            GetAllProductsQuery()));
    }

    [HttpGet]
    [ActionName("Get")]
    public async Task<ActionResult<GetProductResponse>> Get([FromQuery] GetProductQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<ActionResult<AddProductResponse>> Add([FromBody] AddProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [ActionName("Update")]
    public async Task<ActionResult<UpdateProductResponse>> Update([FromBody] UpdateProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete]
    [ActionName("Delete")]
    public async Task<ActionResult<DeleteProductResponse>> Delete([FromQuery] DeleteProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}