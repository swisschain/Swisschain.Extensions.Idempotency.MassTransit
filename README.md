# Swisschain.Extensions.Idempotency.MassTransit
MassTransit implementation of the Idempotency extensions

## Install nuget package

`Install-Package swisschain.extensions.idempotency.MassTransit`

## Initialization

Add `DispatchWithMassTransit` when configuring `OutboxConfigurationBuilder` inside `services.AddOutbox` call:

```c#
services.AddOutbox(c =>
{
    c.DispatchWithMassTransit();
});
```

## Usage

If you working with the Outbox not in the consumer context (eg. HTTP or gRPC request, or timer), then just call `IOutboxManager.EnsureDispatched` passing the `Outbox` instance:

```c#
await _outbox.EnsureDispatched(outbox);
```

If you working with the Outbox in the consumer context (eg. Inside the `IConsumer<T>.Consume`), the it's recommended to pass consumer-scoped dispatcher using `ToOutboxDispatcher` 
extension method of the `ConsumeContext`:

```c#
public class ExecuteTransferConsumer : IConsumer<ExecuteTransfer>
{
    private readonly IOutboxManager _outboxManager;

    public BlockchainAddedConsumer(OutboxManager outboxManager)
    {
        _outboxManager = outboxManager;
    }

    public async Task Consume(ConsumeContext<ExecuteTransfer> context)
    {
        var outbox = _outboxManager.Open($"Commands:ExecuteTransfer:{context.Message.TransferId}");

        // Business logic goes here

        await _outboxManager.EnsureDispatched(outbox, context.ToOutboxDispatcher());
    }
}
```