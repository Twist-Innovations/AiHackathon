using System.Diagnostics.CodeAnalysis;

namespace AiHackathon.ApiService.Validations
{
    public class HandlerResult: HandlerResult<string>
    {
        private HandlerResult() { }
    }
    public class HandlerResult<T> where T : class
    {
        public string Message { get; set; } = string.Empty;

        [MemberNotNullWhen(false, nameof(Errors))]
        [MemberNotNullWhen(true, nameof(Result))]
        public bool IsSuccess => Errors == null || Errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public T? Result { get; set; } = null;
        public Dictionary<string, List<string>>? Errors { get; set; } = null;

        public static HandlerResult<T> Success(T result, string msg)
           => new HandlerResult<T>() { Result = result, Message = msg };
        public static HandlerResult<T> Failure(Dictionary<string, List<string>> error, string msg)
           => new HandlerResult<T>() { Errors = error, Message = msg };
    }
}
