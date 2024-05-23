using MediatR;

namespace Common.CQRS.Requests;

public interface IQuery<out TResponse> : IRequest<TResponse>;