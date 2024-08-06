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

    protected internal Result(string? code, string? message, bool ok)
    {
        if (code == AppConstants.ResponseCodes.Success && !ok)
            throw new InvalidOperationException();
        if (code != AppConstants.ResponseCodes.Success && ok)
            throw new InvalidOperationException();

        Code = code;
        Message = message;
        Ok = ok;
    }

    #region Success
    public static Result Success()
        => new(AppConstants.ResponseCodes.Success, "Success!", true);

    public static Result<TData> Success<TData>(TData data)
        => new(AppConstants.ResponseCodes.Success, "Success!", true, data);
    #endregion

    #region Failure
    public static Result Failure(string message)
        => new(AppConstants.ResponseCodes.Failure, message, false);
    public static Result<TData> Failure<TData>(string message)
        => new(AppConstants.ResponseCodes.Failure, message, false);
    public static Result<TData> Failure<TData>(TData? data, string message)
        => new(AppConstants.ResponseCodes.Failure, message, false, data);
    #endregion

    #region NotFound
    public static Result<string> NotFound(string message)
        => new(AppConstants.ResponseCodes.NotFound, message, false);

    public static Result<TData> NotFound<TData>(string message)
        => new(AppConstants.ResponseCodes.NotFound, message, false);

    public static Result<TData> NotFound<TData>(TData? data, string message)
        => new(AppConstants.ResponseCodes.NotFound, message, false, data);
    #endregion

    #region UnAuthorized
    public static Result<string> UnAuthorized(string message)
        => new(AppConstants.ResponseCodes.UnAuthorized, message, false);

    public static Result<TData> UnAuthorized<TData>(string message)
        => new(AppConstants.ResponseCodes.UnAuthorized, message, false);

    public static Result<TData> UnAuthorized<TData>(TData? data, string message)
        => new(AppConstants.ResponseCodes.UnAuthorized, message, false, data);
    #endregion

    #region UnAuthentication
    public static Result<string> UnAuthentication(string message)
        => new(AppConstants.ResponseCodes.UnAuthentication, message, false);

    public static Result<TData> UnAuthentication<TData>(string message)
        => new(AppConstants.ResponseCodes.UnAuthentication, message, false);

    public static Result<TData> UnAuthentication<TData>(TData? data, string message)
        => new(AppConstants.ResponseCodes.UnAuthentication, message, false, data);
    #endregion

    #region Initialization
    public static Result Initialization(string code, string message, bool oke)
        => new(code, message, oke);

    public static Result<TData> Initialization<TData>(string code, string message, bool oke)
        => new(code, message, oke);

    public static Result<TData> Initialization<TData>(string code, string message, bool oke, TData? data)
        => new(code, message, oke, data);
    #endregion

    #region ValidationError
    public static Result<TData> ValidationError<TData>(TData data)
        => new(AppConstants.ResponseCodes.ValidationError, "Validation error!", false, data);
    #endregion
}

public class Result<TValue> : Result
{
    [JsonPropertyOrder(4)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TValue? Value { get; set; }
    protected internal Result(string code, string message,bool oke, TValue? value)
         : base(code, message, oke)
    {
        this.Value = value;
    }

    protected internal Result(string? code, string? message, bool ok, object? value = null) : base(code, message, ok)
    {
        if (value is not null) throw new Exception("Invalid constructor");
    }
}
