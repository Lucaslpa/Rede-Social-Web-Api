using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.database;
using Blog.Data.repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data.Repository
{
    public class CommentsRepository : Repository<Comment>, ICommentsRepository
    {
        public CommentsRepository( MyDatabaseContext db ) : base( db )
        {
        }

        public async Task<GetAllResponse<Comment>> GetCommentsByPostID( Guid Id , int Page )
        {

            int pageSize = 10;
            int pageNumber = Page;

            var query = DbSet.AsNoTracking().Where( c => c.Post.Id == Id )
                                            .Include( c => c.Post )
                                            .ThenInclude( c => c.User )
                                            .Include( c => c.User );

            var totalRecords = await query.CountAsync();

            var totalPages = (int)Math.Ceiling( totalRecords / (double)pageSize );

            var pagedResults = await query.Skip( (pageNumber - 1) * pageSize )
                                          .Take( pageSize )
                                          .ToListAsync();

            return new GetAllResponse<Comment>
            {
                TotalPages = totalPages ,
                CurrentPage = pageNumber ,
                PageSize = pageSize ,
                TotalRecords = totalRecords ,
                Results = pagedResults
            };
        }

        public async Task<GetAllResponse<Comment>> GetCommentsByUserID( Guid Id , int Page )
        {
            int pageSize = 10;
            int pageNumber = Page;

            var query = DbSet.AsNoTracking()
                        .Where( c => c.User.Id == Id.ToString( "D" ) )
                        .Include( p => p.Post )
                        .ThenInclude( p => p.User )
                        .Include( p => p.User );

            var totalRecords = await query.CountAsync();

            var totalPages = (int)Math.Ceiling( totalRecords / (double)pageSize );

            var pagedResults = await query.Skip( (pageNumber - 1) * pageSize )
                                          .Take( pageSize )
                                          .ToListAsync();

            return new GetAllResponse<Comment>
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