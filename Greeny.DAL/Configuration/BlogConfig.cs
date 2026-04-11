using Greeny.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greeny.Data.Configurations
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Content)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(b => b.Content)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(b => b.Date)
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(b => b.User)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.UserId);

        }
    }
}