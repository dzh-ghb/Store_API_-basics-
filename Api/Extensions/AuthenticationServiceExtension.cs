using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

// расширение для регистрации процесса обработки запросов, содержащих JWT
namespace Api.Extension
{
    public static class AuthenticationServiceExtension
    {
        public static IServiceCollection AddAuthenticationConfig(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var authSettingsToken = configuration["AuthSettings:SecretKey"];

            // регистрирует JWT-аутентификацию
            services.AddAuthentication(u =>
            {
                u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // использование JWT для аутентификации по умолчанию
                u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // что делать, если токен отсутствует/невалиден
            }).AddJwtBearer(u => // поддержка аутентификации через JwtBearer
            {
                // настройка службы
                u.RequireHttpsMetadata = false; //в проде обычно true
                u.SaveToken = true; // сохранение токена в HttpContext
                u.TokenValidationParameters = new TokenValidationParameters // параметры валидации для проверки подлинности JWT
                {
                    ValidateIssuerSigningKey = true, // проверка ключа подписи
                    IssuerSigningKey = new SymmetricSecurityKey( // указание секретного ключа
                        Encoding.ASCII.GetBytes(authSettingsToken)
                    ),
                    ValidateIssuer = false, // отключение проверки издателя токена
                    ValidateAudience = false // отключения проверки назначения токена
                };
            });

            return services;
        }
    }
}