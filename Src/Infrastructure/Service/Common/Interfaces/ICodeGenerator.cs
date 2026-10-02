namespace SwaOlova.Infrastructure.Service.Common.Interfaces;

public interface ICodeGenerator
{
    string GenerateReferralCode();

    string GenerateCouponCode();
}