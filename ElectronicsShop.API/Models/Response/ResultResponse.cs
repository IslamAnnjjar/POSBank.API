namespace ElectronicsShop.API.Models.Response;

public class ResultResponse
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public IList<string>? Errors { get; set; }
}

public class ResultResponse<T> : ResultResponse
{
    public T Content { get; set; } = default!;
}