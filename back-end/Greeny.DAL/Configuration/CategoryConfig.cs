namespace Greeny.DAL.Configuration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}