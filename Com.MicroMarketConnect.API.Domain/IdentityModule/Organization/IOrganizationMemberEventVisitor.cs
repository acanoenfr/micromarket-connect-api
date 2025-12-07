using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public interface IOrganizationMemberEventVisitor<out T>
{
    T Handle(OrganizationMemberAddedEvent @event);
    T Handle(OrganizationMemberUpdatedEvent @event);
    T Handle(OrganizationMemberDeletedEvent @event);
}
