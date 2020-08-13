# Swisschain.Extensions.Idempotency.MassTransit
MassTransit implementation of the Idempotency extensions

## Install nuget package

`Install-Package swisschain.extensions.idempotency.MassTransit`

## Initialization

Add `DispatchWithMassTransit` while configuring `IdempotencyConfigurationBuilder` inside `services.AddIdempotency` call:

```c#
services.AddIdempotency<UnitOfWork>(x =>
{
    x.DispatchWithMassTransit();
});
```

## Usage

### Outside of a consumer context

If you working with the unit of work not in the consumer context (eg. HTTP request, gRPC request, timer), then just call ` IUnitOfWork.EnsureOutboxDispatched()`:

```c#
await unitOfWork.EnsureOutboxDispatched();
```

### Inside a consumer context

If you working with the unit of work in the consumer context (eg. Inside the `IConsumer<T>.Consume`), then it's recommended to pass consumer-scoped dispatcher using `ToOutboxDispatcher` 
extension method of the `ConsumeContext` or using `EnsureOutboxDispatched` extension method of the `UnitOfWorkBase`:

```c#
public class ExecuteTransferConsumer : IConsumer<ExecuteTransfer>
{
    private readonly IUnitOfWorkManager<UnitOfWork> _unitOfWorkManager;

    public BlockchainAddedConsumer(IUnitOfWorkManager<UnitOfWork> unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Consume(ConsumeContext<ExecuteTransfer> context)
    {
        await using var unitOfWork = await UnitOfWorkManager.Begin($"Commands:ExecuteTransfer:{context.Message.TransferId}");

        if (!unitOfWork.Outbox.IsClosed)
        {
            // Business logic goes here

            await unitOfWork.Commit();
        }

        await unitOfWork.EnsureOutboxDispatched(context.ToOutboxDispatcher());
        
        // Or (prefed one):
        
        await unitOfWork.EnsureOutboxDispatched(context);
    }
}
```