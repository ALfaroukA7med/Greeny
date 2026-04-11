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

            // TODO: Configure relationships with User entity for Sender and Receiver

            builder.HasOne(n => n.Receiver)
                .WithMany(u => u.ReceivedNotifications)
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(n => n.Sender)
                .WithMany(s => s.SentNotifications)
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
