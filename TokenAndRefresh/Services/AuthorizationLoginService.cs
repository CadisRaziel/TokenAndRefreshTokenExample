using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TokenAndRefresh.DTO.Request;
using TokenAndRefresh.DTO.Response;
using TokenAndRefresh.Infra;
using TokenAndRefresh.Models;

namespace TokenAndRefresh.Services
{
    public class AuthorizationLoginService : IAuthorizationLoginService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationLoginService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateToken(string idUser)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));

            var credentialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentialsToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreated = tokenHandler.WriteToken(tokenConfig);

            return tokenCreated;
        }


        private string GenerateRefreshToken()
        {

            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        private async Task<AuthorizationResponse> SaveHistoryRefreshToken(
          int idUser,
          string token,
          string refreshToken
          )
        {

            var historialRefreshToken = new HistoryRefreshTokens
            {
                IdUser = idUser,
                Token = token,
                RefreshToken = refreshToken,
                DateCreation = DateTime.UtcNow,
                DateExpiration = DateTime.UtcNow.AddMinutes(2)
            };


            await _context.HistoryRefreshTokens.AddAsync(historialRefreshToken);
            await _context.SaveChangesAsync();

            return new AuthorizationResponse { Token = token, RefreshToken = refreshToken, Result = true, Message = "Ok" };

        }

        public async Task<AuthorizationResponse> ReturnRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUser)
        {
            var refreshTokenFound = _context.HistoryRefreshTokens.FirstOrDefault(x =>
           x.Token == refreshTokenRequest.TokenExpired &&
           x.RefreshToken == refreshTokenRequest.RefreshToken &&
           x.IdUser == idUser);
            
            if (refreshTokenFound == null)
                return new AuthorizationResponse { Result = false, Message = "No exist refreshToken" };
            

            var refreshTokenCreado = GenerateRefreshToken();
            var tokenCreado = GenerateToken(idUser.ToString());

            return await SaveHistoryRefreshToken(idUser, tokenCreado, refreshTokenCreado);
        }

        public async Task<AuthorizationResponse> ReturnToken(AuthorizationRequest authorization)
        {
            var user_found = _context.Users.FirstOrDefault(x =>
               x.UserName == authorization.UserName &&
               x.Password == authorization.Password
           );

            
            if (user_found == null)
            {
                return await Task.FromResult<AuthorizationResponse>(null);
            }
            

            string tokenCreado = GenerateToken(user_found.IdUser.ToString());

            string refreshTokenCreado = GenerateRefreshToken();

            //return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Msg = "Ok" };

            return await SaveHistoryRefreshToken(user_found.IdUser, tokenCreado, refreshTokenCreado);
        }  
    }
}
