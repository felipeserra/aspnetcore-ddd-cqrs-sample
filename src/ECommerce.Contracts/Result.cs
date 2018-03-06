namespace ECommerce.Contracts
{
    public class Result<T> : Result
    {
        public T Data { get; set; }

        public  static Result<T> Fail(string failReason)
        {
            return new Result<T>
            {
                FailReason = failReason,
            };
        }

        public static Result<T> Ok(T data)
        {
            return new Result<T>
            {
                Data = data,
            };
        }
    }

    public class Result
    {
        public string FailReason { get; set; }

        public static Result Fail(string failReason)
        {
            return new Result
            {
                FailReason = failReason,
            };
        }
    }
}
