using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationDeletedEvent(
    RowId Id)
{
}
