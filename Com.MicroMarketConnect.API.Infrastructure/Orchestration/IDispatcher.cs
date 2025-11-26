using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using FluentResults;

namespace Com.MicroMarketConnect.API.Infrastructure.Orchestration;

public interface IDispatcher
{
    Task<Result<IReadOnlyCollection<IDomainEvent>>> Dispatch(ITransactionalStrategy transactionalStrategy, IEventDrivenCommand command);
    Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query);
    Task Publish(IEnumerable<IDomainEvent> domainEvents);
}
