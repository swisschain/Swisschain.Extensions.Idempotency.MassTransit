using System.Threading.Tasks;
using MassTransit;

namespace Swisschain.Extensions.Idempotency.MassTransit
{
    public static class UnitOfWorkExtensions
    {
        public static Task EnsureOutboxDispatched<TUnitOfWork>(this TUnitOfWork unitOfWork,
            ConsumeContext consumeContext)

            where TUnitOfWork : UnitOfWorkBase
        {
            return unitOfWork.EnsureOutboxDispatched(consumeContext.ToOutboxDispatcher());
        }
    }
}
