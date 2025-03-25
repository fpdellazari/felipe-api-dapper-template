using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace FelipeApiDapperTemplate.API.Extensions;

public static class JwtTokenExtensions
{
    public static void AddJwtTokenAuthentication(this WebApplicationBuilder builder, string jwtPrivateKey)
    {
        var privateKey = Encoding.ASCII.GetBytes(jwtPrivateKey);
        builder.Services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context => {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var response = new { message = "Token de autenticação necessário. Forneça um token válido." };
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            };
        });
    }
}

