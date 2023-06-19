using Blog.Business.Models;
using Blog.Business.Services;
using Blog.Data.database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.api.Configuration
{
    public static class IdentityConfiguration
    {

        public static IServiceCollection AddIdentityConfiguration( this IServiceCollection service )
        {
            service.AddIdentity<User , IdentityRole>()
                   .AddEntityFrameworkStores<MyDatabaseContext>()
                   .AddDefaultTokenProviders();
            service.Configure<IdentityOptions>( options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 5 );
                options.Lockout.AllowedForNewUsers = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstçuvwxyzABCDEFGHIJKLMNOPQRSÇTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = true;
            } );

            var serviceProvider = service.BuildServiceProvider();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync( "Admin" ).Result)
            {
                var resultado = roleManager.CreateAsync( new IdentityRole( "Admin" ) ).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception( $"Erro durante a criação da role Admin. Erros: {resultado.Errors}" );
                }
            }

            return service;
        }

    }
}
