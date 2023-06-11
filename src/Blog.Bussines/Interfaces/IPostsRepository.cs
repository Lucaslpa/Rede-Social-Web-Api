using Blog.Business.Models;

namespace Blog.Business.Interfaces
{
    public interface IPostsRepository : IRepository<Post>
    {
        public Task<Post> GetPostByCommentID( Guid Id );
        public Task<GetAllResponse<Post>> GetPostsByUserID( string userId , int Page );
        public Task<Post> GePostCommentUser( Guid Id );
        public Task IncreaseCommentsCount( Guid postId );
        public Task DecreaseCommentsCount( Guid postId );
        public Task IncreaseLikesCount( Guid postId );
        public Task DecreaseLikesCount( Guid postId );

    }
}
