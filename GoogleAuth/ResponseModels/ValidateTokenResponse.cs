namespace GoogleAuth.ResponseModels
{
    public class ValidateTokenResponse : IApiResponse
    {
        public bool TokenIsValid { get; set; }
        public string Message { get; set; }
        public string Error { get; set; } 
    }

}
