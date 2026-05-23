

namespace Greeny.Data.Configurations
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(p => p.Id);

           
            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(5000); 

            //builder.Property(p => p.ImageFile)
            //    .HasMaxLength(255)
            //    .IsRequired(false);

            builder.Property(p => p.Votes)
                .HasDefaultValue(0);

            builder.Property(p => p.Date)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // User Relationship
            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comments Relationship
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}