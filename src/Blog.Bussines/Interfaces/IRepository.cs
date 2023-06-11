using Blog.Business.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add( TEntity entity );
        Task<TEntity> GetByID( Guid id );
        Task<GetAllResponse<TEntity>> GetAll( int Page );
        Task Update( TEntity entity );
        Task Delete( Guid id );
        Task<IEnumerable<TEntity>> Search( Expression<Func<TEntity , bool>> predicate );
        Task<int> SaveChanges();
    }

    public class GetAllResponse<Class>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public List<Class> Results { get; set; }
    }
}
