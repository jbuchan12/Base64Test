using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Base64Test.Test")]
[assembly: InternalsVisibleTo("FluentAssertions.Primitives")]
namespace Base64Test;

/// <summary>
/// Represents a Base64 encoded integer.
/// </summary>
/// <remarks>
/// This class provides methods to convert an integer to a Base64 string and vice versa.
/// </remarks>
internal class Base64Integer
{
    private const string Base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-/";

    /// <summary>
    /// Gets or sets the integer value to be converted to or from a Base64 string.
    /// </summary>
    /// <value>
    /// The integer representation of the Base64 string.
    /// </value>
    public int IntegerValue { get; set; }

    /// <summary>
    /// Gets or sets the Base64 encoded string representation of the integer value.
    /// </summary>
    /// <value>
    /// The Base64 encoded string representation of the integer value.
    /// </value>
    /// <remarks>
    /// This property is used to store the Base64 string equivalent of the integer value.
    /// </remarks>
    public string StringValue { get; set; }

    /// <summary>
    /// Creates a new Base64Integer instance from an integer.
    /// </summary>
    /// <param name="number">The integer to convert to a Base64Integer.</param>
    /// <returns>A new Base64Integer instance representing the given integer.</returns>
    internal static Base64Integer FromInteger(int number) => new(number, ToBase64(number));

    /// <summary>
    /// Creates a new Base64Integer instance from a Base64 encoded string.
    /// </summary>
    /// <param name="base64String">The Base64 encoded string to convert to a Base64Integer.</param>
    /// <returns>A new Base64Integer instance representing the integer value of the given Base64 encoded string.</returns>
    internal static Base64Integer FromString(string base64String) => new(ToInteger(base64String), base64String);

    /// <summary>
    /// Gets the minimum possible value of a Base64Integer.
    /// </summary>
    /// <value>
    /// The minimum possible value of a Base64Integer, which is equivalent to an integer value of 0.
    /// </value>
    /// <remarks>
    /// This property is useful for initializing variables or setting default values.
    /// </remarks>

    internal static Base64Integer Min => new (0, Base64Chars[0].ToString());
    
    private Base64Integer(int number, string base64String)
    {
        IntegerValue = number;
        StringValue = base64String;
    }

    private static int ToInteger(string base64) =>
        base64
            .Select(character => Base64Chars.IndexOf(character, StringComparison.Ordinal))
            .Sum();

    private static string ToBase64(int number)
    {
        if(number == 0)
            return Base64Chars[0].ToString();
        
        var output = string.Empty;
        int divide = number / 64;

        for (var i = 0; i < divide; i++)
            output += Base64Chars[64];

        int remainder = number % 64;
        if (remainder == 0)
            return output;

        output += Base64Chars[remainder];

        return output;
    }
}