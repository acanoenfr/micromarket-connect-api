using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public interface IOrganizationMemberEvent : IDomainEvent
{
    T Accept<T>(IOrganizationMemberEventVisitor<T> visitor);
}
