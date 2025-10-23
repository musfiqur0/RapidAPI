namespace Application.Common;

public class RequestResponse()
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
}
