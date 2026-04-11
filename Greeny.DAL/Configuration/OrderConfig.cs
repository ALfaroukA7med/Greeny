namespace Greeny.DAL.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(o => o.Status)
                .IsRequired();


            // OrderItem relationship
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
