using Com.MicroMarketConnect.API.Core;
using MediatR;

namespace Com.MicroMarketConnect.API.Infrastructure.Database;

public record DomainEventNotification(IDomainEvent Event) : INotification;
