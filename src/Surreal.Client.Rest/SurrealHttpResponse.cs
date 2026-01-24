using System.Net;

namespace Surreal.Client.Rest;

public abstract class SurrealHttpResponseBase
{
    public required int StatusCode { get; init; }
    
    public string? Message { get; init; }
    
    public string? Time { get; init; }

    public bool IsSuccess => HasData() && StatusCode >= 200 && StatusCode <= 299;

    protected abstract bool HasData();
}

/// <summary>
/// Returned when the result is expected to be a single value.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SurrealHttpResponse<T> : SurrealHttpResponseBase
{
    public T? Data { get; init; }
    
    public static SurrealHttpResponse<T> Success(T data, HttpStatusCode code = HttpStatusCode.OK) => 
        new() { Data = data, StatusCode = (int)code };
    
    public static SurrealHttpResponse<T> Failure(string message, HttpStatusCode code) => 
        new() { Message = message, StatusCode = (int) code };

    public static SurrealHttpResponse<T> Success(T data, int code = 200) =>
       new() { Data = data, StatusCode = code };

    public static SurrealHttpResponse<T> Failure(string message, int code) =>
        new() { Message = message, StatusCode = code };

    protected override bool HasData()
    {
        return Data is not null;
    }
}

/// <summary>
/// Returned when the result is expected to be an array.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SurrealHttpArrayResponse<T> : SurrealHttpResponseBase
{
    public T[]? Data { get; init; }
    
    public static SurrealHttpArrayResponse<T> Success(T[] data, HttpStatusCode code = HttpStatusCode.OK) => 
        new() { Data = data, StatusCode = (int)code };
    
    public static SurrealHttpArrayResponse<T> Failure(string message, HttpStatusCode code) => 
        new() { Message = message, StatusCode = (int) code };

    public static SurrealHttpArrayResponse<T> Success(T[] data, int code = 200) =>
       new() { Data = data, StatusCode = code };

    public static SurrealHttpArrayResponse<T> Failure(string message, int code) =>
        new() { Message = message, StatusCode = code };

    protected override bool HasData()
    {
        return Data is not null;
    }
}