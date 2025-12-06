using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public interface IOrganizationEvent : IDomainEvent
{
    T Accept<T>(IOrganizationEventVisitor<T> visitor);
}
