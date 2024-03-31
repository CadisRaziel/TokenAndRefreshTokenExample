namespace TokenAndRefresh.DTO.Request
{
    public class RefreshTokenRequest
    {
        public string TokenExpired { get; set; }
        public string RefreshToken { get; set; }
    }
}
