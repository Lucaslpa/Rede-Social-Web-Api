
using Blog.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class LikesMapping : IEntityTypeConfiguration<Like>
    {
        public void Configure( EntityTypeBuilder<Like> builder )
        {
            builder.HasKey( c => c.Id );

            builder.Property( c => c.UserId )
                .IsRequired()
                .HasColumnType( "varchar" );

            builder.Property( c => c.PostId )
                .IsRequired()
                .HasColumnType( "varchar" );

            builder.HasOne( c => c.Post )
                .WithMany( d => d.Likes )
                .HasForeignKey( c => c.PostId );

            builder.HasOne( c => c.User )
                .WithMany( d => d.Likes )
                .HasForeignKey( c => c.UserId );

            builder.ToTable( "Likes" );
        }

    }
}
