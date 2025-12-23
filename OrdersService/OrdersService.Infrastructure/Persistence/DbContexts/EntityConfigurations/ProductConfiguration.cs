using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Products;

namespace OrdersService.Infrastructure.Persistence.DbContexts.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(p => p.Category)
               .IsRequired();

        builder.Property(p => p.Price)
               .IsRequired();

        builder.Property(p => p.Weight)
               .IsRequired();
    }
}
