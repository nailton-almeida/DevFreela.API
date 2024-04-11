using DevFreela.Core.Enums;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> GeneratePasswordHash256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public async Task<string?> GenerateTokenJWT(string email, int role)
        {
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var key = _configuration["JWT:KeyIssuer"];

            var securityKeySymetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentialstoSigningToken = new SigningCredentials(securityKeySymetric, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("username", email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRoleEnum), role))
            };

            var token = new JwtSecurityToken(
               issuer: issuer,
               audience: audience,
               expires: DateTime.Now.AddHours(8),
               claims: claims,
               signingCredentials: credentialstoSigningToken
               );

            var tokenhandler = new JwtSecurityTokenHandler();

            string tokenString = tokenhandler.WriteToken(token);

            return tokenString;
        }


    }
}
