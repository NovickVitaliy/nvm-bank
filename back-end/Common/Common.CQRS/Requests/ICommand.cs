using MediatR;

namespace Common.CQRS.Requests;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest<Unit>;