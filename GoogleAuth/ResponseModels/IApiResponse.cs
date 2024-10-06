namespace GoogleAuth.ResponseModels
{
    public interface IApiResponse
    {
        string Message { get; set; }
        string Error { get; set; }
    }

}
