namespace GoogleAuth.ResponseModels
{
    public class UserInfoResponse : IApiResponse
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Error { get; set; } 
    }
}
