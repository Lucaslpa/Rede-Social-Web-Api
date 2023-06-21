using AutoMapper;
using Blog.api.ViewModels;
using Blog.Business.Models;
using Blog.Business.Services;
using Microsoft.AspNetCore.Identity;

namespace Blog.api.Seed
{
    public static class SeedUserss
    {
        public async static Task<IServiceCollection> SeedUsers( this IServiceCollection services )
        {

            var serviceProvider = services.BuildServiceProvider();

            var usersMangaer = serviceProvider.GetRequiredService<UserManager<User>>();
            var Mapper = serviceProvider.GetRequiredService<IMapper>();

            var systemDevAcc = await usersMangaer.FindByEmailAsync( "SystemDev1Acc@gmail.com" );

            var user = Mapper.Map<User>( new UserViewModel
            {
                Username = "SystemDevAc1c" ,
                Email = "SystemDev1Acc@gmail.com" ,
                DataCadastro = DateTime.Now ,
                Name = "SystemDev1Acc" ,
                Surname = "SystemDe1vAcc" ,
                BirthDay = DateTime.Now ,
                Password = ""
            } );

            if (systemDevAcc == null)
            {
                var resultado = await usersMangaer.CreateAsync( user , "12345678*lA" );
                var rolesRes = await usersMangaer.AddToRoleAsync( user , "Dev" );
                if (!resultado.Succeeded || !rolesRes.Succeeded)
                {
                    throw new Exception( $"Erro durante a criação do SystemDevAcc;. Erros: {resultado.Errors.First().Description } {rolesRes.Errors.First().Description}" );
                }
            }
            return services;
        }

    }
}
