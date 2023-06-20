using Blog.Business.Services;
using Blog.Data.database;
using Microsoft.OpenApi.Models;

namespace Blog.api.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection Configure( this IServiceCollection Services , IConfiguration configuration )

        {
            // Add services to the container.
            Services.AddNewtonSoftConfiguration();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1" , new OpenApiInfo { Title = "Rede social - V1" , Version = "v1" } );
                c.SwaggerDoc( "v2" , new OpenApiInfo { Title = "Rede social - V2" , Version = "v2" } );
            } );
            Services.AddDependencyInjection();
            Services.AddAuthorization();
            Services.AddDbContext<MyDatabaseContext>();
            Services.ConfigureVersioning();
            Services.AddIdentityConfiguration();
            Services.AddAutoMapperProfiles();
            Services.ConfigureJwt( configuration );
            Services.AddCors();


            return Services;
        }
    }
}
