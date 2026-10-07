using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.AddOrderItem;

public sealed record AddOrderItemCommand(Guid OrderId, AddOrderItemRequest Request)
    : CommandBase<AddOrderItemResponse>;
