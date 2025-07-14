using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.SalesDeveloper.Domain.Entities;

namespace Mouts.SalesDeveloper.Infrastructure.Mapping
{
    public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("sale_items");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductName).IsRequired();
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.Discount).HasColumnType("decimal(18,2)");
        }
    }
}
