using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Infrastructure.Database;

public interface IDomainEventsRepository
{
    Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents);
}

public class DomainEventsRepository : IDomainEventsRepository
{
    private readonly MicroMarketConnectDbContext _dbContext;
    private readonly IEnumerable<IDomainEventRepository> _repositories;

    public DomainEventsRepository(
        MicroMarketConnectDbContext dbContext,
        IEnumerable<IDomainEventRepository> repositories)
    {
        _dbContext = dbContext;
        _repositories = repositories;
    }

    public async Task ApplyEvents(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await Task.WhenAll(
                _repositories.Select(repo => repo.ApplyEvent(domainEvent))
            );
        }

        await _dbContext.SaveChangesAsync();
    }
}
