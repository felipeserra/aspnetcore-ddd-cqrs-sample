namespace ECommerce.Domain
{
    public class ApplicationResponse<T> : ApplicationResponse
    {
        public T Data { get; set; }

        public  static ApplicationResponse<T> Fail(StatusCode statusCode, string failReason)
        {
            return new ApplicationResponse<T>
            {
                StatusCode = statusCode,
                FailReason = failReason,
                IsSuccesful = false,
            };
        }

        public static ApplicationResponse<T> Success(T data)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                IsSuccesful = true,
            };
        }
    }

    public class ApplicationResponse
    {
        public bool IsSuccesful { get; set; }
        public StatusCode StatusCode { get; set; }
        public string FailReason { get; set; }

        public static ApplicationResponse Success()
        {
            return new ApplicationResponse
            {
                IsSuccesful = true,
            };
        }
        public static ApplicationResponse Fail(StatusCode statusCode, string failReason)
        {
            return new ApplicationResponse
            {
                StatusCode = statusCode,
                FailReason = failReason,
                IsSuccesful = false,
            };
        }
    }
}
