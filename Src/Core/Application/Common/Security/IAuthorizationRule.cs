namespace SwaOlova.Application.Common.Security;

public interface IAuthorizationRule
{
    string Policy { get; }
}