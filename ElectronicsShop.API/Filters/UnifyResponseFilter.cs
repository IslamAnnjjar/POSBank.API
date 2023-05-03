using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ElectronicsShop.API.Filters;

public class UnifyResponseFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (((IStatusCodeActionResult)context.Result) == null)
        {
            return;
        }

        var actionResultStatusCode = ((IStatusCodeActionResult)context.Result).StatusCode;
        var successCodes = new List<int?> { 200, 201 };

        if (successCodes.Contains(actionResultStatusCode))
        {
            var returnObj = ((ObjectResult)context.Result);
            context.Result = new ResponseActionResult(new ResultResponse() { Content = returnObj.Value, IsSuccess = true, StatusCode = returnObj.StatusCode });
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}

public class ResultResponse
{
    public bool IsSuccess { get; set; }
    public int? StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public IList<string>? Errors { get; set; }
    public Object? Content { get; set; }
}

public class ResponseActionResult : IActionResult
{
    private readonly ResultResponse _responseMessage;
    public ResponseActionResult()
    {
    }

    public ResponseActionResult(ResultResponse responseMessage)
    {
        _responseMessage = responseMessage; // could add throw if null
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_responseMessage);

        await objectResult.ExecuteResultAsync(context);
    }
}

