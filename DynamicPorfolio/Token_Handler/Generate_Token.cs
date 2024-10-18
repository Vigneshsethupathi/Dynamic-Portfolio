using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Token_Handler
{
    public class Generate_Token
    {
        private const string SecretKey = "YourGeneratedBase64EncodedSecretKey";
        private readonly SymmetricSecurityKey _signingKey;

        public Generate_Token()
        {
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }

        public (string token, DateTime expiration) GenerateToken(string email, int expireMinutes = 3)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var expiration = now.AddMinutes(expireMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return (token, expiration);
        }
    }


}

