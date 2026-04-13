namespace Greeny.DAL.Configuration
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(300);

            //Blog comments relation 

            builder.HasOne(c => c.Post)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.PostId);
        }
    }
}