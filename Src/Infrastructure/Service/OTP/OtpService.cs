using System.Security.Cryptography;

namespace SwaOlova.Infrastructure.Service.OTP;

public sealed class OtpService : IOtpService
{
    private const int OtpDigits = 6;
    private static readonly TimeSpan DefaultLifetime = TimeSpan.FromMinutes(5);

    public string GenerateOtp()
    {
        var value = RandomNumberGenerator.GetInt32(0, 1_000_000);
        return value.ToString("D6");
    }

    public bool ValidateOtp(string otp, string expectedOtp)
        => string.Equals(otp?.Trim(), expectedOtp?.Trim(), StringComparison.Ordinal);

    public DateTime ExpireOtp(DateTime generatedAt, TimeSpan? lifetime = null)
        => generatedAt.Add(lifetime ?? DefaultLifetime);
}