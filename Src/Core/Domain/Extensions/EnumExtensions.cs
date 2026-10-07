using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SwaOlova.Domain.Extensions;

/// <summary>
/// Extension methods for enum types.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the display name from the Display attribute of an enum value.
    /// If no Display attribute is found, returns the enum value's name.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The display name or the enum value's name if no Display attribute is found.</returns>
    public static string GetDisplayName(this Enum value)
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString()).FirstOrDefault();

        if (memberInfo is null)
        {
            return value.ToString();
        }

        var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute?.Name ?? value.ToString();
    }
}
