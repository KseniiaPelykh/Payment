using System.Collections.Generic;

namespace Payment.Core.Utility
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public IReadOnlyCollection<string> Errors { get; }

        private Result(T value, bool success, IReadOnlyCollection<string> errors = null)
        {
            Value = value;
            IsSuccess = success;
            Errors = errors ?? new List<string>();
        }

        public static Result<T> CreateSuccess(T value) =>
            new Result<T>(value, true);

        public static Result<T> CreateFailure() =>
             new Result<T>(default(T), false);

        public static Result<T> CreateFailure(T value) =>
            new Result<T>(value, false);

        public static Result<T> CreateFailure(IReadOnlyCollection<string> errors) =>
            new Result<T>(default(T), false, errors);
    }
}
