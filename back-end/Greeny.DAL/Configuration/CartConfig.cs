namespace Greeny.DAL.Configuration
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);  

        }
    }
}
