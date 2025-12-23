using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Orders;
using OrdersService.Domain.Products;

namespace OrdersService.Infrastructure.DbContexts.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(i => i.ItemId);

        builder.Property(i => i.OrderId).IsRequired();
        builder.Property(i => i.ProductId).IsRequired();
        builder.Property(i => i.PriceOnOrder).IsRequired();
        builder.Property(i => i.Quantity).IsRequired();

        builder.HasIndex(i => new { i.OrderId, i.ProductId }).IsUnique();

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(oi => oi.ProductId)
               .IsRequired();
    }
}
