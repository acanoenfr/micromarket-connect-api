using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;
using Com.MicroMarketConnect.API.Infrastructure.Database;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

public class UserDomainEventRepository(
    IIdentityDbContext dbContext,
    IDateProvider dateProvider) : DomainEventRepository<IUserEvent>, IUserEventVisitor<Task>
{
    protected override Task ApplyEvent(IUserEvent domainEvent) => domainEvent.Accept(this);

    public async Task Handle(UserAddedEvent @event)
    {
        var entity = new UserEntity()
        {
            Id = @event.Id.Value,
            DiplayName = @event.DisplayName.Value,
            Email = @event.Email.Value,
            PasswordHash = @event.PasswordHash.Value,
            PasswordSalt = @event.PasswordSalt.Value,
            CreatedAt = dateProvider.NewDate()
        };

        await dbContext.Users.AddAsync(entity);
    }
}
