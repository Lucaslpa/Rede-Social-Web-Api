using Blog.Data.database;

namespace Blog.api.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection Configure( this IServiceCollection Services, IConfiguration configuration )

        {
            // Add services to the container.
            Services.AddNewtonSoftConfiguration();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddDependencyInjection();
            Services.AddAuthorization();
            Services.AddDbContext<MyDatabaseContext>();
            Services.AddIdentityConfiguration();
            Services.AddAutoMapperProfiles();
            Services.ConfigureJwt( configuration );
            Services.AddCors();

            return Services;
        }
    }
}
