using MassTransit;

namespace Swisschain.Extensions.Idempotency.MassTransit
{
    public static class ConsumingContextExtensions
    {
        public static IOutboxDispatcher ToOutboxDispatcher(this ConsumeContext context)
        {
            return new ConsumingContextOutboxDispatcher(context);
        }
    }
}
