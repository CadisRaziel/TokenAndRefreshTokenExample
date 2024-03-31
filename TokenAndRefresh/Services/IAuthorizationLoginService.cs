using TokenAndRefresh.DTO.Request;
using TokenAndRefresh.DTO.Response;

namespace TokenAndRefresh.Services
{
    public interface IAuthorizationLoginService
    {
        Task<AuthorizationResponse> ReturnToken(AuthorizationRequest authorization);
        Task<AuthorizationResponse> ReturnRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUser);
    }
}
