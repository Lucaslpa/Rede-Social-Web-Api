using Blog.Business.Models;


namespace Blog.Business.Interfaces
{
    public interface ILikesService
    {
        public Task<bool> AddLike( Like like );
    }
}
