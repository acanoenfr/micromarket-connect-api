using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using FluentResults;

namespace Com.MicroMarketConnect.API.Infrastructure.Orchestration;

public class WebDispatcher
{
    private readonly IDispatcher _dispatcher;

    public WebDispatcher(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query) => _dispatcher.Dispatch(query);

    public async Task<Result<IReadOnlyCollection<IDomainEvent>>> Dispatch(IEventDrivenCommand command)
    {
        var domainEvents = await _dispatcher.Dispatch(new RootTransactionalStrategy(), command);
        await Publish(domainEvents.ValueOrDefault ?? Enumerable.Empty<IDomainEvent>());
        return domainEvents;
    }

    public Task Publish(IEnumerable<IDomainEvent> domainEvents) => _dispatcher.Publish(domainEvents);
}
