using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerByPhoneNumber;

public sealed record GetCustomerByPhoneNumberQuery(string PhoneNumber)
    : QueryBase<GetCustomerByPhoneNumberResponse>;
