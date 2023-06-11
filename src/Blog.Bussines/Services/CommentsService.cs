using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.ModelValidation;
using Blog.Business.Notifications;


namespace Blog.Business.Services
{
    public class CommentsService : Service, ICommentsService
    {

        ICommentsRepository CommentsRepository;
        IPostsRepository PostsRepository;

        public CommentsService( INotifier notifier , ICommentsRepository commentsRepository , IPostsRepository postsRepository ) : base( notifier )
        {
            CommentsRepository = commentsRepository;
            PostsRepository = postsRepository;
        }

        public async Task<bool> Add( Comment comment )
        {

            if (!Validate( new CommentValidation() , comment ))
                return false;
            await CommentsRepository.Add( comment );
            await PostsRepository.IncreaseCommentsCount( comment.PostId );
            return true;
        }

        public async Task<bool> Update( Comment comment )

        {

            if (!Validate( new CommentValidation() , comment ))
                return false;

            await CommentsRepository.Update( comment );

            return true;
        }

    }
}
