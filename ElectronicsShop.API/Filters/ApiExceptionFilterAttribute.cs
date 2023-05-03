using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ElectronicsShop.Application.Common.Exceptions;

namespace ElectronicsShop.API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly JsonSerializerSettings _serializerSettings;

    public ApiExceptionFilterAttribute(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            { typeof(ApplicationException), HandleAppException },
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        _httpContextAccessor.HttpContext!.Response.ContentType = "application/json";
        _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;

        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;

        var result = JsonConvert.SerializeObject(new ElectronicsShop.API.Models.Response.ResultResponse
        {
            ErrorMessage = exception.Message,
            IsSuccess = false,
            StatusCode = StatusCodes.Status400BadRequest,
            Errors = exception.Errors.Values.SelectMany(x => x).ToList()
        }, _serializerSettings);

        _httpContextAccessor.HttpContext!.Response.WriteAsync(result);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as ElectronicsShop.Application.Common.Exceptions.NotFoundException;

        var result = JsonConvert.SerializeObject(new ElectronicsShop.API.Models.Response.ResultResponse
        {
            ErrorMessage = exception!.Message,
            IsSuccess = false,
            StatusCode = StatusCodes.Status404NotFound
        }, _serializerSettings);

        _httpContextAccessor.HttpContext!.Response.WriteAsync(result);

        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var result = JsonConvert.SerializeObject(new ElectronicsShop.API.Models.Response.ResultResponse
        {
            ErrorMessage = "Unauthorized",
            IsSuccess = false,
            StatusCode = StatusCodes.Status401Unauthorized
        }, _serializerSettings);

        _httpContextAccessor.HttpContext!.Response.WriteAsync(result);

        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var exception = context.Exception as ForbiddenAccessException;

        var result = JsonConvert.SerializeObject(new ElectronicsShop.API.Models.Response.ResultResponse
        {
            ErrorMessage = string.IsNullOrEmpty(exception!.Message) ? "Forbidden" : exception.Message,
            IsSuccess = false,
            StatusCode = StatusCodes.Status403Forbidden
        }, _serializerSettings);

        _httpContextAccessor.HttpContext!.Response.WriteAsync(result);

        context.ExceptionHandled = true;
    }

    private void HandleAppException(ExceptionContext context)
    {
        var result = JsonConvert.SerializeObject(new ElectronicsShop.API.Models.Response.ResultResponse
        {
            ErrorMessage = context.Exception.Message,
            IsSuccess = false,
            StatusCode = StatusCodes.Status400BadRequest
        }, _serializerSettings);

        _httpContextAccessor.HttpContext!.Response.WriteAsync(result);

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var result = JsonConvert.SerializeObject(new ElectronicsShop.API.Models.Response.ResultResponse
        {
            ErrorMessage = "An error occurred while processing your request.",
            IsSuccess = false,
            StatusCode = StatusCodes.Status500InternalServerError
        }, _serializerSettings);

        _httpContextAccessor.HttpContext!.Response.WriteAsync(result);

        context.ExceptionHandled = true;
    }
}