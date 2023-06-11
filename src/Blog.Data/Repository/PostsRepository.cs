using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.database;
using Blog.Data.repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data.Repository
{
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        public PostsRepository( MyDatabaseContext db ) : base( db )
        {
        }

        public async Task IncreaseCommentsCount( Guid postId )
        {
            var post = await DbSet.AsNoTracking().FirstOrDefaultAsync( p => p.Id == postId );
            post.CommentsCount += 1;
            DbSet.Update( post );
            await SaveChanges();
        }

        public async Task IncreaseLikesCount( Guid postId )
        {
            var post = await DbSet.AsNoTracking().FirstOrDefaultAsync( p => p.Id == postId );
            post.LikesCount += 1;
            DbSet.Update( post );
            await SaveChanges();
        }

        public async Task DecreaseLikesCount( Guid postId )
        {
            var post = await DbSet.AsNoTracking().FirstOrDefaultAsync( p => p.Id == postId );
            post.LikesCount -= 1;
            DbSet.Update( post );
            await SaveChanges();
        }

        public async Task DecreaseCommentsCount( Guid postId )
        {
            var post = await DbSet.AsNoTracking().FirstOrDefaultAsync( p => p.Id == postId );
            post.CommentsCount -= 1;
            DbSet.Update( post );
            await SaveChanges();
        }

        public async Task<Post> GetPostByCommentID( Guid Id )
        {
            return await DbSet.AsNoTracking()
                              .Include( p => p.Comments )
                              .Where( p => p.Comments.Any( p => p.Id == Id ) )
                              .FirstOrDefaultAsync();

        }
        public async Task<GetAllResponse<Post>> GetPostsByUserID( string userId , int Page )
        {
            int pageSize = 10;
            int pageNumber = Page;

            var query = DbSet.AsNoTracking().Where( c => c.UserId == userId )
                                            .Include( c => c.User );

            var totalRecords = await query.CountAsync();

            var totalPages = (int)Math.Ceiling( totalRecords / (double)pageSize );

            var pagedResults = await query.Skip( (pageNumber - 1) * pageSize )
                                          .Take( pageSize )
                                          .ToListAsync();

            return new GetAllResponse<Post>
            {
                TotalPages = totalPages ,
                CurrentPage = pageNumber ,
                PageSize = pageSize ,
                TotalRecords = totalRecords ,
                Results = pagedResults
            };
        }

        public async Task<Post> GePostCommentUser( Guid Id )
        {
            return await Db.Posts.AsNoTracking()
                                .Include( p => p.User )
                                .Include( p => p.Comments )
                                .FirstOrDefaultAsync( p => p.Id == Id );

        }

    }




}
