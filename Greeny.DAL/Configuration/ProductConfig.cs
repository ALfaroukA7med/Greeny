namespace Greeny.DAL.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasMaxLength(2000); 

            builder.Property(p => p.Quantity)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Category Relationship
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Reviews Relationship
            builder.HasMany(p => p.Reviews)
                   .WithOne(r => r.Product)
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);


            // OrderItems Relationship
            builder.HasMany(p => p.OrderItems)
                   .WithOne(oi => oi.Product)
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

            // CartItems Relationship
            builder.HasMany(p => p.CartItems)
                   .WithOne(ci => ci.Product)
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
