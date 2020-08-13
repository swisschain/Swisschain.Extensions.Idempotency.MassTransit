using MassTransit;

namespace Swisschain.Extensions.Idempotency.MassTransit
{
    public static class UnitOfWorkExtensions
    {
        public static TUnitOfWork EnsureOutboxDispatched<TUnitOfWork>(this TUnitOfWork unitOfWork,
            ConsumeContext consumeContext)

            where TUnitOfWork : UnitOfWorkBase
        {
            unitOfWork.EnsureOutboxDispatched(consumeContext.ToOutboxDispatcher());

            return unitOfWork;
        }
    }
}
