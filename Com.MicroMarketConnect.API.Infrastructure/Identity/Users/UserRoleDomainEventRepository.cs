using Com.MicroMarketConnect.API.Domain.IdentityModule.User;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

public class UserRoleDomainEventRepository(
    IIdentityDbContext dbContext) : DomainEventRepository<IUserRoleEvent>, IUserRoleEventVisitor<Task>
{
    protected override Task ApplyEvent(IUserRoleEvent domainEvent) => domainEvent.Accept(this);

    public async Task Handle(UserRoleAddedEvent @event)
    {
        var entity = new UserRoleEntity()
        {
            UserId = @event.UserId.Value,
            RoleName = @event.RoleName.Value
        };

        await dbContext.UserRoles.AddAsync(entity);
    }

    public async Task Handle(UserRoleDeletedEvent @event)
    {
        var toDelete = await dbContext.UserRoles
            .FirstOrDefaultAsync(ur =>
                ur.UserId.Equals(@event.UserId.Value) &&
                ur.RoleName.Equals(@event.RoleName.Value));

        if (toDelete is not null)
            dbContext.UserRoles.Remove(toDelete);
    }
}
