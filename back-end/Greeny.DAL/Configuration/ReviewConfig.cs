namespace Greeny.DAL.Configuration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Stars)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(r => r.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.Date)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Relationships

            // Review Relationships
            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product Relationships
            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}