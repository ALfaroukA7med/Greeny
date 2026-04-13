namespace Greeny.DAL.Configuration
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.Date)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Receiver relationship (required)
            builder.HasOne(n => n.Receiver)
                .WithMany(u => u.Notifications) 
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sender relationship (optional)
            builder.HasOne(n => n.Sender)
                .WithMany() 
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.NoAction); // ضروري جداً لتجنب Multiple Cascade Paths
        }
    }
}
