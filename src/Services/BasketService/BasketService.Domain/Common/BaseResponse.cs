namespace BasketService.Domain.Common;

public class BaseResponse
{
    public bool IsSucceed { get; private set; }
    public string Message { get; private set; }
    public int StatusCode { get; private set; }

    public BaseResponse(bool isSucceed, string message, int statusCode)
    {
        IsSucceed = isSucceed;
        Message = message;
        StatusCode = statusCode;
    }
    public BaseResponse(bool isSucceed, int statusCode) : this(isSucceed, string.Empty, statusCode) { }
}

public class ErrorResponse : BaseResponse
{
    public ErrorResponse(string Message) : base(false, Message, 400) { }
}

public class SuccessResponse : BaseResponse
{
    public SuccessResponse(string Message) : base(true, Message, 200) { }
    public SuccessResponse() : base(true, 200) { }
}
