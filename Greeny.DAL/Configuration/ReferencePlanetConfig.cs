namespace Greeny.DAL.Configuration
{
    public class ReferencePlanetConfig : IEntityTypeConfiguration<ReferencePlanet>
    {
        public void Configure(EntityTypeBuilder<ReferencePlanet> builder)
        {
            builder.ToTable("ReferencePlanets");
            builder.HasKey(rp => rp.Id);

            builder.Property(rp => rp.CommonName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(rp=>rp.SciName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(rp => rp.Family)
               .IsRequired()
               .HasMaxLength(255);

            builder.Property(rp => rp.Family)
              .IsRequired()
              .HasMaxLength(255);


            builder.Property(rp => rp.Image)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(rp => rp.Description)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(rp => rp.PlanetType)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(rp => rp.Image)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(rp => rp.GrowthSeason)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(rp => rp.SunlightReq)
              .IsRequired();

            builder.Property(rp => rp.SolidType)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(rp => rp.WaterReq)
             .IsRequired();

            builder.Property(rp => rp.TempReq)
             .IsRequired();

        }
    }
}