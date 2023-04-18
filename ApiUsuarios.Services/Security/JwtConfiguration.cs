using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiUsuarios.Services.Security
{
    public class JwtConfiguration
    {
        /// <summary>
        /// Método para configurar o tipo de autenticação do projeto API
        /// de forma a utilizar o framework JWT
        /// </summary>
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(
                auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenCreator.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}



