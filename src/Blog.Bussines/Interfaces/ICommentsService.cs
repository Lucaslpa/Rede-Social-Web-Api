using Blog.Business.Models;

namespace Blog.Business.Interfaces
{
    public interface ICommentsService
    {
        public Task<bool> Add( Comment comment );
        public Task<bool> Update( Comment comment );
    }
}
