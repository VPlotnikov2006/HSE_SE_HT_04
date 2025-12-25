using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsService.Application.Outbox;

namespace PaymentsService.Infrastructure.Persistence.DbContexts.EntityConfigurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox");

        builder.HasKey(x => x.OrderId);

        builder.Property(x => x.NewStatus)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Published)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp");

        builder.Property(x => x.PublishedAt)
            .HasColumnType("timestamp");
    }
}
