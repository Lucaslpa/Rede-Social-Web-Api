using Blog.Business.Interfaces;
using Blog.Business.Notifications;
using Blog.Business.Services;
using Blog.Data.database;
using Blog.Data.Repository;

namespace Blog.api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection( this IServiceCollection services )
        {
            services.AddScoped<MyDatabaseContext>();
            services.AddScoped<INotifier , Notifier>();
            services.AddScoped<IPostsService , PostsService>();
            services.AddScoped<IPostsRepository , PostsRepository>();
            services.AddScoped<ICommentsService , CommentsService>();
            services.AddScoped<ICommentsRepository , CommentsRepository>();
            services.AddScoped<ILikeRespository , LikesRepository>();
            services.AddScoped<ILikesService , LikesService>();

            return services;
        }
    }
}
