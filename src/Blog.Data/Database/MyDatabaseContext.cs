using Blog.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Data.Mappings;
using System.Reflection;

namespace Blog.Data.database
{
    public class MyDatabaseContext : IdentityDbContext<User>
    {
        public MyDatabaseContext()
        {
            string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData );
            DbPath = Path.Combine( appDataPath , "blog.db" ); ;
        }

        protected override void OnConfiguring( DbContextOptionsBuilder options )
        => options.UseSqlite( $"Data Source={DbPath};Mode=ReadWrite;" );

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new CommentMapping() );
            modelBuilder.ApplyConfiguration( new PostMapping() );
            modelBuilder.ApplyConfiguration( new LikesMapping() );

            base.OnModelCreating( modelBuilder );
        }

        public string DbPath { get; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
