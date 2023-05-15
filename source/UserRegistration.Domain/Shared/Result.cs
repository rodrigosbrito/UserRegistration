namespace UserRegistration.Domain.Shared
{
    public enum ResultType { Success, Failure }

    public record Result(ResultType Type, string Message, List<string> Errors)
    {
        public bool HasErrors => Errors?.Count > 0;

        public bool IsSuccess => Type == ResultType.Success;

        public bool IsFailure => Type == ResultType.Failure;

        public static Result Success() => new(ResultType.Success, null, null);

        public static Result Failure(string error) => new(ResultType.Failure, null, new List<string> { error });

        public Result WithMessage(string message) => this with { Message = message };

        public Result WithErrors(List<string> errors) => this with { Errors = errors };

    }

    public record Result<T>(ResultType Type, string Message, List<string> Errors, T Value) : Result(Type, Message, Errors)
    {
        public bool HasValue => Value is not null;

        public static new Result<T> Success(T value) => new(ResultType.Success, null, null, value);

        public static new Result<T> Failure(string error) => new(ResultType.Failure, null, new List<string> { error }, default);
        public static new Result<T> Failure(List<string> errors) => new(ResultType.Failure, null, errors, default);
        public Result<TU> Convert<TU>() => new(Type, Message, Errors, (TU)(object)Value);
    }
}

