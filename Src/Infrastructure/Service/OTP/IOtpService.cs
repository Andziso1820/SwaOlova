namespace SwaOlova.Infrastructure.Service.OTP;

public interface IOtpService
{
    string GenerateOtp();

    bool ValidateOtp(string otp, string expectedOtp);

    DateTime ExpireOtp(DateTime generatedAt, TimeSpan? lifetime = null);
}