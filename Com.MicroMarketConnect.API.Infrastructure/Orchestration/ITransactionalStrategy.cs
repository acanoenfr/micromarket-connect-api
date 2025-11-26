namespace Com.MicroMarketConnect.API.Infrastructure.Orchestration;

public interface ITransactionalStrategy
{
    Task ExecuteTransactional(Func<Task> function);
}
