using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.database;
using Blog.Data.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class LikesRepository : Repository<Like>, ILikeRespository
    {
        public LikesRepository( MyDatabaseContext db ) : base( db )
        {
        }

        public Task<Like> getLike( string UserId , Guid PostId )
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync( p => p.UserId == UserId && p.PostId == PostId );
        }


        public async Task<GetAllResponse<Like>> GetLikesByPostID( Guid postId , int Page )
        {
            int pageSize = 10;
            int pageNumber = Page;

            var query = DbSet.AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling( totalRecords / (double)pageSize );

            var pagedResults = await query.Skip( (pageNumber - 1) * pageSize )
                                          .Take( pageSize )
                                          .Where( p => p.PostId == postId )
                                          .Include( p => p.Post )
                                            .ThenInclude( p => p.User )
                                          .Include( p => p.User )
                                          .ToListAsync();

            return new GetAllResponse<Like>
            {
                TotalPages = totalPages ,
                CurrentPage = pageNumber ,
                PageSize = pageSize ,
                TotalRecords = totalRecords ,
                Results = pagedResults
            };
        }
        public async Task<GetAllResponse<Like>> GetLikesByUserID( Guid userId , int Page )
        {
            int pageSize = 10;
            int pageNumber = Page;

            var query = DbSet.AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling( totalRecords / (double)pageSize );

            var pagedResults = await query.Skip( (pageNumber - 1) * pageSize )
                                          .Take( pageSize )
                                          .Where( p => p.UserId == userId.ToString( "D" ) )
                                          .Include( p => p.Post )
                                            .ThenInclude( p => p.User )
                                          .Include( p => p.User )
                                          .ToListAsync();

            return new GetAllResponse<Like>
            {
                TotalPages = totalPages ,
                CurrentPage = pageNumber ,
                PageSize = pageSize ,
                TotalRecords = totalRecords ,
                Results = pagedResults
            };
        }
    }
}
