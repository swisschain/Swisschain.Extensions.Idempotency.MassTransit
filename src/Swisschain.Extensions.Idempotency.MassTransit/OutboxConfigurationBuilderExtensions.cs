using Microsoft.Extensions.DependencyInjection;

namespace Swisschain.Extensions.Idempotency.MassTransit
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
