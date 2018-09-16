﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataBaseContext.Map
{
    public class PlayerActivationMap : IEntityTypeConfiguration<PlayerActivation>
    {
        public void Configure(EntityTypeBuilder<PlayerActivation> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.PlayerId).IsUnique(true);
        }
    }
}