using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public class OrganizationMemberDomainEventRepository(
    IIdentityDbContext dbContext) : DomainEventRepository<IOrganizationMemberEvent>, IOrganizationMemberEventVisitor<Task>
{
    protected override Task ApplyEvent(IOrganizationMemberEvent domainEvent) => domainEvent.Accept(this);

    public async Task Handle(OrganizationMemberAddedEvent @event)
    {
        var entity = new OrganizationMemberEntity()
        {
            OrganizationId = @event.OrganizationId.Value,
            UserId = @event.UserId.Value,
            Role = @event.Role.Value
        };

        await dbContext.OrganizationMembers.AddAsync(entity);
    }

    public async Task Handle(OrganizationMemberUpdatedEvent @event)
    {
        var toEdit = await dbContext.OrganizationMembers
            .FirstOrDefaultAsync(om =>
                om.OrganizationId.Equals(@event.OrganizationId.Value) &&
                om.UserId.Equals(@event.UserId.Value));

        if (toEdit is not null)
        {
            toEdit.Role = @event.Role.Value;
        }
    }

    public async Task Handle(OrganizationMemberDeletedEvent @event)
    {
        var toRemove = await dbContext.OrganizationMembers
            .FirstOrDefaultAsync(om =>
                om.OrganizationId.Equals(@event.OrganizationId.Value) &&
                om.UserId.Equals(@event.UserId.Value));

        if (toRemove is not null)
            dbContext.OrganizationMembers.Remove(toRemove);
    }
}
