using System.Security.Cryptography;
using System.Text;
using SwaOlova.Infrastructure.Service.Common.Interfaces;

namespace SwaOlova.Infrastructure.Service.Common;

public sealed class CodeGenerator : ICodeGenerator
{
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public string GenerateReferralCode() => $"REF-{GenerateCode(8)}";

    public string GenerateCouponCode() => $"CPN-{GenerateCode(10)}";

    private static string GenerateCode(int length)
    {
        var buffer = new StringBuilder(length);

        for (var index = 0; index < length; index++)
        {
            buffer.Append(Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)]);
        }

        return buffer.ToString();
    }
}