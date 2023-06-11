using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.database;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MyDatabaseContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository( MyDatabaseContext db )
        {
            Db = db;
            DbSet = db.Set<TEntity>();

        }

        public async Task Add( TEntity entity )
        {
            DbSet.Add( entity );
            await SaveChanges();
        }
        public async Task Delete( Guid id )
        {
            DbSet.Remove( new TEntity { Id = id } );
            await SaveChanges();
        }
        public async Task<GetAllResponse<TEntity>> GetAll( int Page )
        {

            int pageSize = 10;
            int pageNumber = Page;

            var query = DbSet.AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling( totalRecords / (double)pageSize );

            var pagedResults = await query.Skip( (pageNumber - 1) * pageSize )
                                          .Take( pageSize )
                                          .ToListAsync();

            return new GetAllResponse<TEntity>
            {
                TotalPages = totalPages ,
                CurrentPage = pageNumber ,
                PageSize = pageSize ,
                TotalRecords = totalRecords ,
                Results = pagedResults
            };
        }
        public async Task<TEntity> GetByID( Guid id )
        {
            return await DbSet.FindAsync( id );
        }
        public async Task<IEnumerable<TEntity>> Search( System.Linq.Expressions.Expression<Func<TEntity , bool>> predicate )
        {
            return await DbSet.AsNoTracking().Where( predicate ).ToListAsync();
        }
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public async Task Update( TEntity entity )
        {
            DbSet.Update( entity );
            await SaveChanges();
        }
        public void Dispose()
        {
            Db.Dispose();
        }

    }
}
