using MeChat.Common.Constants;
using System.Text.Json.Serialization;

namespace MeChat.Common.Shared.Response;
public class Result
{
    [JsonPropertyOrder(1)]
    public string? Code { get; set; }
    [JsonPropertyOrder(2)]
    public string? Message { get; set; }
    [JsonPropertyOrder(3)]
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

    public static Result<TData> Failure<TData>(TData? data, string message)
        => new(ResponseCodes.Failure, message, false, data);

    public static Result<string> NotFound(string message)
        => new(ResponseCodes.NotFound, "NotFound" , false, message);

    public static Result<string> UnAuthorized(string message)
        => new(ResponseCodes.UnAuthorized, "UnAuthorized", false, message);
    public static Result<string> UnAuthentication(string message)
    => new(ResponseCodes.UnAuthentication, "UnAuthentication", false, message);

    public static Result Initialization(string code, string message, bool oke)
        => new(code,message, oke);

    public static Result<TData> Initialization<TData>(string code, string message, bool oke, TData? data)
    => new(code, message, oke, data);

    public static Result<TData> ValidationError<TData>(TData data)
    {
        return new(ResponseCodes.ValidationError, "Validation error!", false, data);
    }

}

public class Result<TValue> : Result
{
    [JsonPropertyOrder(4)]
    public TValue? Value { get; set; }
    protected internal Result(string code, string message,bool oke, TValue? value)
         : base(code, message, oke)
    {
        this.Value = value;
    }
}