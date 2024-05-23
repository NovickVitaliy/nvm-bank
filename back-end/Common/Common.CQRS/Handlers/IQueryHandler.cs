using Common.CQRS.Requests;
using MediatR;

namespace Common.CQRS.Handlers;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;