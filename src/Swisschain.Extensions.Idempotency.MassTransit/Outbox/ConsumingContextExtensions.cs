using MassTransit;
using Swisschain.Extensions.Idempotency.Outbox;

namespace Swisschain.Extensions.Idempotency.MassTransit.Outbox
{
    public static class ConsumingContextExtensions
    {
        public static IOutboxDispatcher ToOutboxDispatcher(this ConsumeContext context)
        {
            return new ConsumingContextOutboxDispatcher(context);
        }
    }
}
