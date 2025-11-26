namespace Com.MicroMarketConnect.API.Infrastructure.Orchestration;

public class NestedTransactionalStrategy : ITransactionalStrategy
{
    public Task ExecuteTransactional(Func<Task> function)
        => function();
}
