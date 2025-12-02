using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public interface IOrganizationEventVisitor<out T>
{
    T Handle(OrganizationAddedEvent @event);
    T Handle(OrganizationUpdatedEvent @event);
    T Handle(OrganizationDeletedEvent @event);
}
