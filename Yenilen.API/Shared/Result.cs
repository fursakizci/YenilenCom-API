namespace Yenilen.API.Shared;

public class Result<T>
{
    public bool IsSuccess { get; }
    public int StatusCode { get; }
    public T? Data { get; }
    public List<string>? Errors { get; }

    private Result(bool isSuccess, int statusCode, T? data, List<string>? errors)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Data = data;
        Errors = errors;
    }

    public static Result<T> Success(T data, int statusCode = 200)
    {
        return new Result<T>(true, statusCode, data, null);
    }

    public static Result<T> Failure(int statusCode, List<string>? errors = null)
    {
        return new Result<T>(false, statusCode, default, errors ?? new List<string>());
    }

    public static Result<T> Failure(string errorMessage, int statusCode = 500)
    {
        return new Result<T>(false, statusCode, default, new List<string> { errorMessage });
    }
}