using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Common.Abstractions;

public abstract record CommandBase : IRequest<Result>;

public abstract record CommandBase<TResponse> : IRequest<Result<TResponse>>;

public abstract record QueryBase<TResponse> : IRequest<Result<TResponse>>;