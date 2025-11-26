using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Infrastructure.Database;

public interface IDomainEventRepository
{
    Task ApplyEvent(IDomainEvent domainEvent);
}

public abstract class DomainEventRepository<TDomainEvent>() : IDomainEventRepository
    where TDomainEvent : IDomainEvent
{
    public Task ApplyEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            TDomainEvent typedEvent => ApplyEvent(typedEvent),
            _ => Task.CompletedTask
        };
    }

    protected abstract Task ApplyEvent(TDomainEvent domainEvent);
}
