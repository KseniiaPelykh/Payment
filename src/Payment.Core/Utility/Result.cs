namespace Payment.Core.Utility
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }

        private Result(T value, bool success)
        {
            Value = value;
            IsSuccess = success;
        }

        public static Result<T> CreateSuccess(T value) =>
            new Result<T>(value, true);

        public static Result<T> CreateFailure() =>
             new Result<T>(default(T), false);
    }
}
