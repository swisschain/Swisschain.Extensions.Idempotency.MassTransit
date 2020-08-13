using Microsoft.Extensions.DependencyInjection;

namespace Swisschain.Extensions.Idempotency.MassTransit
{
    public static class OutboxConfigurationBuilderExtensions
    {
        public static IdempotencyConfigurationBuilder<TUnitOfWork> DispatchWithMassTransit<TUnitOfWork>(this IdempotencyConfigurationBuilder<TUnitOfWork> builder) 
            where TUnitOfWork : UnitOfWorkBase
        {
            builder.Services.AddTransient<IOutboxDispatcher, RootOutboxDispatcher>();

            return builder;
        }
    }
}
