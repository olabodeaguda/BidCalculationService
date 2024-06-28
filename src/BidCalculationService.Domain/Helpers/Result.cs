using BidCalculationService.Domain.Exceptions;

namespace BidCalculationService.Domain.Helpers
{
    public class Result<T>
    {
        protected Result(bool isSuccess, Error? error, T? data)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }

        public bool IsSuccess { get; }
        public Error? Error { get; }
        public T? Data { get; }
        public static Result<T> Success(T data, Error? error = null) => new(true, error, data);
        public static Result<T> Failure(Error error) => new(false, error, default);

    }

    public class Result : Result<string>
    {
        protected Result(bool isSuccess, Error? error, string? data)
            : base(isSuccess, error, data)
        {
        }

        public static new Result Success(string data, Error? error = null) => new(true, error, data);
        public static new Result Failure(Error error) => new(false, error, null);
    }
}
