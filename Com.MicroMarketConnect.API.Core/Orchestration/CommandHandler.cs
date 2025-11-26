using FluentResults;
using MediatR;

namespace Com.MicroMarketConnect.API.Core.Orchestration;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Result<IReadOnlyCollection<IDomainEvent>>>
    where TCommand : IEventDrivenCommand
{
    public async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(TCommand command, CancellationToken cancellationToken)
        => await Handle(command);
    protected abstract Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(TCommand command);
}
