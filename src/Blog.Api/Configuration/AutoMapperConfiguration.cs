using AutoMapper;
using Blog.api.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;

namespace Blog.api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post , PostViewModel>()
                .ForMember( dest => dest.Id , opt => opt.Condition( src => src.Id != Guid.Empty ) )
                .ReverseMap();

            CreateMap<Comment , CommentViewModel>()
                 .ForMember( dest => dest.Id , opt => opt.Condition( src => !src.Id.Equals( Guid.Empty ) ) )
                 .ReverseMap();

            CreateMap<GetAllResponse<Comment> , GetAllResponse<CommentViewModel>>()
                 .ReverseMap();

            CreateMap<GetAllResponse<Post> , GetAllResponse<PostViewModel>>()
                 .ReverseMap();


            CreateMap<Like , LikesViewModel>()
                .ReverseMap();


            CreateMap<GetAllResponse<Like> , GetAllResponse<LikesViewModel>>()
                .ReverseMap();

            CreateMap<User , UserViewModel>()
                .ReverseMap();
        }
    }

    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperProfiles( this IServiceCollection services )
        {

            services.AddAutoMapper( conf =>
            {
                conf.AddProfile<AutoMapperProfile>();
            } );

            return services;
        }

    }
}
