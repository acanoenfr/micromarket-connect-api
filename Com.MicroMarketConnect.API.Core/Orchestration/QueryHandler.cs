using MediatR;

namespace Com.MicroMarketConnect.API.Core.Orchestration;

public abstract class QueryHandler<T, TResult> : IRequestHandler<T, TResult>
    where T : IRequest<TResult>
{
    public Task<TResult> Handle(T query, CancellationToken cancellationToken)
        => Handle(query);
    protected abstract Task<TResult> Handle(T query);
}
