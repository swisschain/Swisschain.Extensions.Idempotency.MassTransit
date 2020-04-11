using Microsoft.Extensions.DependencyInjection;
using Swisschain.Extensions.Idempotency.Outbox;

namespace Swisschain.Extensions.Idempotency.MassTransit.Outbox
{
    public static class OutboxConfigurationBuilderExtensions
    {
        public static OutboxConfigurationBuilder DispatchWithMassTransit(this OutboxConfigurationBuilder builder)
        {
            builder.Services.AddTransient<IOutboxDispatcher, RootOutboxDispatcher>();

            return builder;
        }
    }
}
