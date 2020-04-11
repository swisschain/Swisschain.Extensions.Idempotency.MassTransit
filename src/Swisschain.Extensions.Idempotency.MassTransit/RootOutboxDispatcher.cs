using System.Threading.Tasks;
using MassTransit;

namespace Swisschain.Extensions.Idempotency.MassTransit
{
    internal sealed class RootOutboxDispatcher : IOutboxDispatcher
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public RootOutboxDispatcher(ISendEndpointProvider sendEndpointProvider,
            IPublishEndpoint publishEndpoint)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public Task Send(object command)
        {
            return _sendEndpointProvider.Send(command);
        }

        public Task Publish(object evt)
        {
            return _publishEndpoint.Publish(evt);
        }
    }
}
