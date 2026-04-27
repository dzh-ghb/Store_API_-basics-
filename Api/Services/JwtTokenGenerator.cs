using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Model;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    // сервис генерации JWT
    public class JwtTokenGenerator
    {
        private readonly string secretKey;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            this.secretKey = configuration["AuthSettings:SecretKey"];
        }

        // в параметрах объекты с необходимыми для payload данными (юзер, список ролей)
        public string GenerateJwtToken(AppUser appUser, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); // конвертация ключа в массив битов

            // дескриптор токена (содержит всю инфу, которая будет включаться в токен)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // поле с утверждениями о юзере (payload)
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("FirstName", appUser.FirstName),
                    new Claim("id", appUser.Id),
                    new Claim(ClaimTypes.Email, appUser.Email), // возможно использование заранее описанных полей
                    new Claim(ClaimTypes.Role, String.Join(", ", roles))
                }),

                // поле со временем истечения срока действия токена
                Expires = DateTime.UtcNow.AddDays(1),

                // поле с алгоритмом шифрования и ключом, которыми будет подписан токен
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            // получение токена
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}