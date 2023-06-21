using Microsoft.OpenApi.Models;

namespace Blog.api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection SwaggerConfig( this IServiceCollection services )
        {

            services.AddSwaggerGen( c =>
            {



                c.AddSecurityDefinition( "Bearer" , new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"" ,
                    Name = "Authorization" ,
                    In = ParameterLocation.Header ,
                    Type = SecuritySchemeType.ApiKey ,
                    Scheme = "Bearer"
                } );


                c.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme ,
                                Id = "Bearer"
                            }
                        } ,
                        new string[] { }
                    }
                } );

                c.SwaggerDoc( "v1" , new OpenApiInfo { Title = "Rede social - V1" , Version = "v1" } );
                c.SwaggerDoc( "v2" , new OpenApiInfo { Title = "Rede social - V2" , Version = "v2" } );
            } );

            return services;
        }

    }
}
