namespace TokenAndRefresh.DTO.Response
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
