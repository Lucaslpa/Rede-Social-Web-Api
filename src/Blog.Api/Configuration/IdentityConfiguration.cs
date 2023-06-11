using Blog.Business.Models;
using Blog.Data.database;
using Microsoft.AspNetCore.Identity;

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
            return service;
        }

    }
}
