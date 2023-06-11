using Blog.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface ILikeRespository : IRepository<Like>
    {

        public Task<GetAllResponse<Like>> GetLikesByPostID( Guid postId , int Page );

        public Task<GetAllResponse<Like>> GetLikesByUserID( Guid userId , int Page );

        public Task<Like> getLike( string UserId , Guid PostId );
    }
}
