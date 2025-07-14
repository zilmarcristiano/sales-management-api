﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mouts.SalesDeveloper.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Mouts.SalesDeveloper.Infrastructure.Mapping
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sales");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SaleNumber).IsRequired();
            builder.Property(s => s.SaleDate).IsRequired();
            builder.Property(s => s.CustomerName).IsRequired();
            builder.Property(s => s.BranchName).IsRequired();

            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey("SaleId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
