namespace SwaOlova.Portal.Models;

public sealed class AjaxModalResult
{
    public bool Succeeded { get; init; }

    public string Message { get; init; } = string.Empty;

    public string? RedirectUrl { get; init; }
}
