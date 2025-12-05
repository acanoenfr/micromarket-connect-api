using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public record Organization(
    RowId Id,
    Name Name,
    DisplayName DisplayName,
    Description Description,
    CreatedAt CreatedAt)
{
    public static Organization Hydrate(
        RowId Id,
        Name Name,
        DisplayName DisplayName,
        Description Description,
        CreatedAt CreatedAt)
        => new(Id, Name, DisplayName, Description, CreatedAt);

    public static IEnumerable<IDomainEvent> Create(
        RowId Id,
        Name Name,
        DisplayName DisplayName,
        Description Description)
    {
        yield return new OrganizationAddedEvent(Id, Name, DisplayName, Description);
    }

    public static IEnumerable<IDomainEvent> Update(
        RowId Id,
        Name Name,
        DisplayName DisplayName,
        Description Description)
    {
        yield return new OrganizationUpdatedEvent(Id, Name, DisplayName, Description);
    }

    public static IEnumerable<IDomainEvent> Delete(
        RowId Id)
    {
        yield return new OrganizationDeletedEvent(Id);
    }
}
