namespace SwaOlova.Application.Common.Interfaces.Services;

public interface IWhatsAppService
{
    Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default);
}