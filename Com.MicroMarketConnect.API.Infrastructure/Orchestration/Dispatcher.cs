using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using FluentResults;
using MediatR;

namespace Com.MicroMarketConnect.API.Infrastructure.Orchestration;

public class Dispatcher : IDispatcher
{
    private readonly IMediator _mediator;
    private readonly IDomainEventsRepository _domainEventsRepository;

    public Dispatcher(
        IMediator mediator,
        IDomainEventsRepository domainEventsRepository)
    {
        _mediator = mediator;
        _domainEventsRepository = domainEventsRepository;
    }

    public async Task<Result<IReadOnlyCollection<IDomainEvent>>> Dispatch(ITransactionalStrategy transactionalStrategy, IEventDrivenCommand command)
    {
        var results = await _mediator.Send(command, CancellationToken.None);

        await transactionalStrategy.ExecuteTransactional(async () =>
        {
            await _domainEventsRepository.ApplyEvents(results.ValueOrDefault ?? Enumerable.Empty<IDomainEvent>());
        });

        return results;
    }

    public async Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> query) => await _mediator.Send(query);

    public Task Publish(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var @event in domainEvents)
            _mediator.Publish(new DomainEventNotification(@event));
        return Task.CompletedTask;
    }
}
