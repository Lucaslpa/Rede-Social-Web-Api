using Blog.Business.Services;
using Microsoft.AspNetCore.Identity;

namespace Blog.api.Seed
{
    public static class SeedRoless
    {
        public static IServiceCollection SeedRoles( this IServiceCollection services )
        {

            var serviceProvider = services.BuildServiceProvider();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync( "Admin" ).Result)
            {
                var resultado = roleManager.CreateAsync( new IdentityRole( "Admin" ) ).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception( $"Erro durante a criação da role Admin. Erros: {resultado.Errors}" );
                }
            }

            if (!roleManager.RoleExistsAsync( "Dev" ).Result)
            {
                var resultado = roleManager.CreateAsync( new IdentityRole( "Dev" ) ).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception( $"Erro durante a criação da role Admin. Erros: {resultado.Errors}" );
                }
            }

            return services;
        }

    }
}
