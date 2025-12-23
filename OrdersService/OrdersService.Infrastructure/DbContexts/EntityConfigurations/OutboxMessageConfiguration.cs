using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Application.Outbox;

namespace OrdersService.Infrastructure.DbContexts.EntityConfigurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.Debit).IsRequired();
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.Published).IsRequired();
        builder.Property(o => o.PublishedAt);

        builder.HasIndex(o => new { o.Published, o.CreatedAt })
               .HasDatabaseName("IX_Outbox_Unpublished_CreatedAt")
               .HasFilter("\"Published\" = false");
    }
}
