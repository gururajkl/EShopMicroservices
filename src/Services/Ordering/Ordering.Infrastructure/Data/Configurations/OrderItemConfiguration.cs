using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Model;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
            .HasConversion(oi => oi.Value, dbId => OrderItemId.Of(dbId));

        // Foreign key configurations.
        builder.HasOne<Product>()
            .WithMany().HasForeignKey(oi => oi.ProductId);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();
    }
}
