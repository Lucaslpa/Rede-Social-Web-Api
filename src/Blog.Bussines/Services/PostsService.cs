using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.ModelValidation;
using Microsoft.Extensions.Hosting;


namespace Blog.Business.Services
{
    public class PostsService : Service, IPostsService
    {
        IPostsRepository PostsRepository;
        public PostsService( INotifier notifier , IPostsRepository postsRepository ) : base( notifier )
        {
            PostsRepository = postsRepository;
        }

        public async Task<bool> Add( Post post )
        {

            if (!Validate( new PostValidation() , post ))
                return false;

            await PostsRepository.Add( post );
            return true;
        }

        public async Task<bool> Update( Post post )
        {
            if (!Validate( new PostValidation() , post ))
                return false;
            await PostsRepository.Update( post );
            return true;
        }

    }
}
