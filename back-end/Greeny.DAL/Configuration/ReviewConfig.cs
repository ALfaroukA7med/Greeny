namespace Greeny.DAL.Configuration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            // TODO: Uinque ID
            builder.HasKey(r => r.Id);

            // TODO: Add Range
            builder.Property(r => r.Stars)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(r => r.Content)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}