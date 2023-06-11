
using Blog.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    internal class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure( EntityTypeBuilder<Post> builder )
        {
            builder.HasKey( c => c.Id );

            builder.Property( c => c.Img )
             .HasColumnType( "varchar" );

            builder.Property( c => c.Text )
             .IsRequired()
             .HasColumnType( "varchar(200)" );

            builder.Property( c => c.LikesCount )
             .IsRequired()
             .HasColumnType( "int" );

            builder.Property( c => c.CommentsCount )
             .IsRequired()
             .HasColumnType( "int" );

            builder.Property( c => c.UserId )
             .IsRequired()
             .HasColumnType( "varchar" );

            builder.HasOne( c => c.User )
                .WithMany( c => c.Posts )
                .HasForeignKey( c => c.UserId );

            builder.HasMany( c => c.Comments )
                   .WithOne( c => c.Post )
                   .HasForeignKey( c => c.PostId );


            builder.ToTable( "Posts" );
        }

    }
}
