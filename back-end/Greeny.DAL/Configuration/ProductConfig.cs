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
                .HasMaxLength(255);

            builder.Property(p => p.Quantity)
                .IsRequired();

       
            builder.Property(p => p.Price)
               .IsRequired();

            //Category relation
            builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);

            //Review relation
            builder.HasMany(p => p.Reviews)
               .WithOne(r => r.Product)
               .HasForeignKey(r => r.ProductId);

            //OrderItem relation
            builder.HasOne(p => p.OrderItem)
               .WithOne(oi => oi.Product)
               .HasForeignKey<Product>(p => p.OrderItemId);

            //CartItem relation
            builder.HasOne(p => p.CartItem)
               .WithOne(ci => ci.Product)
               .HasForeignKey<Product>(p => p.CartItemId);


        }
    }
}
