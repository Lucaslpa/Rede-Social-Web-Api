using Blog.api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.api.Configuration
{


    public static class JwtConfiguration
    {
        public static IServiceCollection ConfigureJwt( this IServiceCollection services , IConfiguration configuration )
        {
            var jwtSettings = configuration.GetSection( "JwtSettings" ).Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes( jwtSettings.SecretKey );

            services.AddAuthentication( options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } )
            .AddJwtBearer( options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true ,
                    IssuerSigningKey = new SymmetricSecurityKey( key ) ,
                    ValidateIssuer = true ,
                    ValidIssuer = jwtSettings.Issuer ,
                    ValidateAudience = true ,
                    ValidAudience = jwtSettings.Issuer ,
                };
            } );

            return services;

        }
    }
}
