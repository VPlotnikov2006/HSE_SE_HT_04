using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PaymentsService.Application.DTOs;
using PaymentsService.Application.Interfaces.Messaging;

namespace PaymentsService.Infrastructure.Messaging.Kafka;

public sealed class KafkaMessageConsumer : IMessageConsumer, IDisposable
{
    private readonly IConsumer<string, string> _consumer;
    private readonly KafkaOptions _options;

    public KafkaMessageConsumer(IOptions<KafkaOptions> options)
    {
        _options = options.Value;

        var config = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            GroupId = _options.ConsumerGroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe(_options.OrdersOutboxTopic);
    }

    public async Task ConsumeAsync(
        Func<Guid, UpdateBalanceCommand, CancellationToken, Task> handler,
        CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            ConsumeResult<string, string>? result = null;

            try
            {
                result = _consumer.Consume(ct);

                var msg = JsonSerializer.Deserialize<OrdersOutboxMessageDto>(result.Message.Value)!;

                var command = new UpdateBalanceCommand(
                    msg.UserId, 
                    -msg.TotalPrice
                );

                await handler(msg.OrderId, command, ct);

                _consumer.Commit(result);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KafkaConsumer][Error] {ex.Message}");
                await Task.Delay(1000, ct);
            }
        }
    }

    public void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
    }

    private record OrdersOutboxMessageDto(
        Guid OrderId,
        Guid UserId,
        decimal TotalPrice
    );
}
