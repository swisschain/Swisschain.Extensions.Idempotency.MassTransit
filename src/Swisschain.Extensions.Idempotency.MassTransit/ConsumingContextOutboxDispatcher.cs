using System.Threading.Tasks;
using MassTransit;

namespace Swisschain.Extensions.Idempotency.MassTransit
{
    internal sealed class ConsumingContextOutboxDispatcher : IOutboxDispatcher
    {
        private readonly ConsumeContext _consumeContext;

        public ConsumingContextOutboxDispatcher(ConsumeContext consumeContext)
        {
            _consumeContext = consumeContext;
        }

        public Task Send(object command)
        {
            return _consumeContext.Send(command);
        }

        public Task Publish(object evt)
        {
            return _consumeContext.Publish(evt);
        }
    }
}
