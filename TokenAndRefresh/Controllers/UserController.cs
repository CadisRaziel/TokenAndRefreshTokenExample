using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TokenAndRefresh.DTO.Request;
using TokenAndRefresh.DTO.Response;
using TokenAndRefresh.Services;

namespace TokenAndRefresh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthorizationLoginService _authorizationLoginService;

        public UserController(IAuthorizationLoginService authorizationLoginService)
        {
            _authorizationLoginService = authorizationLoginService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthorizationRequest authorization)
        {
            var authorization_result = await _authorizationLoginService.ReturnToken(authorization);
            if (authorization_result == null)
                return Unauthorized();

            return Ok(authorization_result);
        }


        [HttpPost]
        [Route("GetRefreshToken")]
        public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequest request)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpiredSupposedly = tokenHandler.ReadJwtToken(request.TokenExpired);

            if (tokenExpiredSupposedly.ValidTo > DateTime.UtcNow)
                return BadRequest(new AuthorizationResponse { Result = false, Message = "Token has not expired" });

            string IdUser = tokenExpiredSupposedly.Claims.First(x =>
                x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();


            var autorizacionResponse = await _authorizationLoginService.ReturnRefreshToken(request, int.Parse(IdUser));

            if (autorizacionResponse.Result)
                return Ok(autorizacionResponse);
            else
                return BadRequest(autorizacionResponse);
        }

    }
}
