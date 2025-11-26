namespace Com.MicroMarketConnect.API.Core.Orchestration;

public interface IDomainEventIntoCommand : IDomainEvent
{
    IEventDrivenCommand ToCommand();
}
