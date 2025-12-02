using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public class OrganizationDomainEventRepository(
    IIdentityDbContext dbContext,
    IGuidProvider guidProvider,
    IDateProvider dateProvider) : DomainEventRepository<IOrganizationEvent>, IOrganizationEventVisitor<Task>
{
    protected override Task ApplyEvent(IOrganizationEvent domainEvent) => domainEvent.Accept(this);

    public async Task Handle(OrganizationAddedEvent @event)
    {
        var entity = new OrganizationEntity()
        {
            Id = guidProvider.NewGuid(),
            Name = @event.Name.Value,
            DisplayName = @event.DisplayName.Value,
            Description = @event.Description.Value,
            CreatedAt = dateProvider.NewDate()
        };

        await dbContext.Organizations.AddAsync(entity);
    }

    public async Task Handle(OrganizationUpdatedEvent @event)
    {
        var toEdit = await dbContext.Organizations
            .FirstOrDefaultAsync(o => o.Id.Equals(@event.Id.Value));

        if (toEdit is not null)
        {
            toEdit.Name = @event.Name.Value;
            toEdit.DisplayName = @event.DisplayName.Value;
            toEdit.Description = @event.Description.Value;
        }
    }

    public async Task Handle(OrganizationDeletedEvent @event)
    {
        var toRemove = await dbContext.Organizations
            .FirstOrDefaultAsync(o => o.Id.Equals(@event.Id.Value));

        if (toRemove is not null)
            dbContext.Organizations.Remove(toRemove);
    }
}
