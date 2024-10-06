namespace GoogleAuth.ResponseModels
{
    public class ErrorResponse : IApiResponse
    {
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
