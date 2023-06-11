
using Blog.Business.Models;

namespace Blog.Business.Interfaces
{
    public interface ICommentsRepository : IRepository<Comment>
    {

        public Task<GetAllResponse<Comment>> GetCommentsByPostID( Guid Id , int Page );
        public Task<GetAllResponse<Comment>> GetCommentsByUserID( Guid Id , int Page );

    }
}
