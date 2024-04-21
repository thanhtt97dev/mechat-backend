using MeChat.Common.Constants;

namespace MeChat.Common.Shared.Response;
public class Result
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public bool Ok { get; set; }

    public Result(string? code, string? message, bool ok)
    {
        if (code == ResponseCodes.Success && !ok)
            throw new InvalidOperationException();
        if (code != ResponseCodes.Success && ok)
            throw new InvalidOperationException();

        Code = code;
        Message = message;
        Ok = ok;
    }

    public static Result Success()
        =>new (ResponseCodes.Success, "Success!", true);

    public static Result<TData> Success<TData>(TData data)
        => new(ResponseCodes.Success, "Success!", true, data);

    public static Result Failure(string message) 
        => new(ResponseCodes.Failure, message, false);
    public static Result<TData> Failure<TData>(TData data, string message)
    => new(ResponseCodes.Failure, message, false, data);

    public static Result<TData> ValidationError<TData>(TData data)
    {
        return new Result<TData>(ResponseCodes.ValidationErrors, "Validation error!", false, data);
    }

}

public class Result<TValue> : Result
{
    public TValue? Value { get; set; }
    protected internal Result(string code, string message,bool oke, TValue? value)
         : base(code, message, oke)
    {
        this.Value = value;
    }
}