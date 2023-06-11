using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.ModelValidation;

namespace Blog.Business.Services
{
    public class LikesService : Service, ILikesService
    {
        readonly ILikeRespository LikeRespository;
        readonly IPostsRepository PostsRepository;
        public LikesService( INotifier notifier , ILikeRespository likeRespository , IPostsRepository postsRepository ) : base( notifier )
        {
            LikeRespository = likeRespository;
            PostsRepository = postsRepository;
        }


        public async Task<bool> AddLike( Like like )
        {
            if (!Validate( new LikeValidation() , like ))
                return false;

            await PostsRepository.IncreaseLikesCount( like.PostId );
            await LikeRespository.Add( like );
            return true;
        }

    }
}
