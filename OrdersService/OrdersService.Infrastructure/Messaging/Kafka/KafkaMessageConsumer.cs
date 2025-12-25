using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrdersService.Application.DTOs;
using OrdersService.Application.Interfaces.Messaging;

namespace OrdersService.Infrastructure.Messaging.Kafka;

public sealed class KafkaMessageConsumer : IMessageConsumer, IDisposable
{
    private readonly IConsumer<string, string> _consumer;

    public KafkaMessageConsumer(IOptions<KafkaOptions> options)
    {
        var cfg = options.Value;

        var config = new ConsumerConfig
        {
            BootstrapServers = cfg.BootstrapServers,
            GroupId = cfg.ConsumerGroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        Console.WriteLine($"[DEBUG]: {config.BootstrapServers} | {config.GroupId}");
        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe(cfg.PaymentStatusTopic);
    }

    public async Task ConsumeAsync(
        Func<UpdateOrderStatusCommand, CancellationToken, Task> handler,
        CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            ConsumeResult<string, string>? result = null;

            try
            {
                result = _consumer.Consume(ct);

                var command = JsonSerializer.Deserialize<UpdateOrderStatusCommand>(
                    result.Message.Value)!;

                await handler(command, ct);

                _consumer.Commit(result);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), ct);
            }
        }
    }

    public void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
    }
}

