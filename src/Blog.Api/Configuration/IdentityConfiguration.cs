using Blog.api.Seed;
using Blog.Business.Models;
using Blog.Business.Services;
using Blog.Data.database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.api.Configuration
{
    public static class IdentityConfiguration
    {

        public async static Task<IServiceCollection> AddIdentityConfiguration( this IServiceCollection service )
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

            service.SeedRoles();

            await service.SeedUsers();

            return service;
        }

    }
}
