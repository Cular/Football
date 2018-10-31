using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataBaseContext.Map
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Token).IsUnique();
        }
    }
}
