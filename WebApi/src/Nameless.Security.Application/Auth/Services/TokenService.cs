using Microsoft.IdentityModel.Tokens;
using Nameless.Core.Domain.Extensions;
using Nameless.Infra.DbEntities;
using Nameless.Security.Application.Auth.Configuration;
using Nameless.Security.Application.AuthServices.Interfaces;
using Nameless.Security.Application.AuthServices.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nameless.Security.Application.Auth.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public TokenService(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public string GenerateAuthenticationToken(AccountModel account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", account.Id.ToString()),
                    new Claim("Email", account.Email),
                    new Claim("Username", account.Username),
                    new Claim("Language", account.Language.GetEnumDisplayDescription())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtConfiguration.ExpiresInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ValidatedToken ValidateToken(string? token)
        {
            if (string.IsNullOrEmpty(token)) return GetInvalidToken();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return GetValidToken(jwtToken.Claims);
            }
            catch
            {
                return GetInvalidToken();
            }
        }

        private static ValidatedToken GetValidToken(IEnumerable<Claim> claims)
        {
            return new ValidatedToken()
            {
                IsValid = true,
                Account = new AuthenticatedAccount()
                {
                    Id = Guid.Parse(claims.First(x => x.Type == "Id").Value),
                    Email = claims.First(x => x.Type == "Email").Value,
                    Username = claims.First(x => x.Type == "Username").Value,
                    Language = claims.First(x => x.Type == "Language").Value
                }
            };
        }

        private static ValidatedToken GetInvalidToken()
        {
            return new ValidatedToken()
            {
                IsValid = false
            };
        }
    }
}
