using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MangoFood.Service.OrderAPI.Utilities
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var jwtOptions = builder.Configuration.GetSection("ApiSettings:JwtOptions");
            var secret = jwtOptions.GetValue<string>("Secret");
            var issuer = jwtOptions.GetValue<string>("Issuer");
            var audience = jwtOptions.GetValue<string>("Audience");

            if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException("JWT configuration is missing or invalid.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });
            return builder;
        }
    }
}
