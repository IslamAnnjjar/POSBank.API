using ElectronicsShop.API.Models.Response;
using ElectronicsShop.Application.Features.Categories.Commands.AddCategory;
using ElectronicsShop.Application.Features.Orders.Queries.GetAllOrders;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsShop.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpGet]
    [ActionName("GetAll")]
    public async Task<ActionResult<GetAllOrdersResponse>> GetAll()
    {
        return Ok(await Mediator.Send(new GetAllOrdersQuery()));
    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<ActionResult<AddOrderResponse>> Add([FromBody] AddOrderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}