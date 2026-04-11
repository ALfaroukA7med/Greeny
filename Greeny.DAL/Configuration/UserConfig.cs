namespace Greeny.DAL.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProfilePicture).HasMaxLength(255);
            builder.Property(x => x.Address).HasMaxLength(255); 
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            // Bolgs relationship
            builder.HasMany(u => u.Blogs)
           .WithOne(b => b.User)
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Cascade);

            // Comments relationship
            builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);

            // Notifications relationship
            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.Receiver)
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reviews relationship
            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // Orders relationship
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payments relationship
            builder.HasMany(u => u.Payments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Cart relationship
            builder.HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}