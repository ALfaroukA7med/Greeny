namespace Greeny.DAL.Configuration
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Method)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue("Pending"); 

            builder.Property(p => p.PaidAt)
                .IsRequired(false);

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            
            builder.Property(p => p.TransactionRef)
                .IsRequired(false)
                .HasMaxLength(255);

            // Orders relationship - One-to-One
            builder.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users relationship - One-to-Many
            builder.HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}

