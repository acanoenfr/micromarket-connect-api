using FluentResults;
using MediatR;

namespace Com.MicroMarketConnect.API.Core.Orchestration;

public interface IEventDrivenCommand : IRequest<Result<IReadOnlyCollection<IDomainEvent>>>
{
}
