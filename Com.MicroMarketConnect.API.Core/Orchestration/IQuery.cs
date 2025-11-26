using MediatR;

namespace Com.MicroMarketConnect.API.Core.Orchestration;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
