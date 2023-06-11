
using Blog.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    internal class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure( EntityTypeBuilder<Comment> builder )
        {
            builder.HasKey( c => c.Id );
            builder.Property( c => c.Text )
                .IsRequired()
                .HasColumnType( "varchar(200)" );
            builder.Property( c => c.Date )
                .IsRequired()
                .HasColumnType( "datetime" );
            builder.Property( c => c.UserId )
                .IsRequired()
                .HasColumnType( "varchar" );

            builder.HasOne( c => c.User )
                .WithMany( d => d.Comments )
                .HasForeignKey( c => c.UserId );

            builder.ToTable( "Comments" );
        }

    }
}
