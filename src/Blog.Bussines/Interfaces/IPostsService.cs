using Blog.Business.Models;


namespace Blog.Business.Interfaces
{
    public interface IPostsService
    {

        Task<bool> Add( Post post );
        Task<bool> Update( Post post );
    }
}
