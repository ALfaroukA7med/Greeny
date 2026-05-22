using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.DAL.Configuration
{
    public class VoteConfig : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(v => new { v.UserId, v.PostId })
            .IsUnique();
        }
    }
}
