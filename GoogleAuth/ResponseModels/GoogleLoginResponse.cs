namespace GoogleAuth.ResponseModels
{
    public class GoogleLoginResponse : IApiResponse
    {
        public string Token { get; set; }
        public string Message { get; set; } 
        public string Error { get; set; } 
    }
}
