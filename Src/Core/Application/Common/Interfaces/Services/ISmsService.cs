namespace SwaOlova.Application.Common.Interfaces.Services;

public interface ISmsService
{
    Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default);
}