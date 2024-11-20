using Kursio.Common.Domain;
using MediatR;

namespace Kursio.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
