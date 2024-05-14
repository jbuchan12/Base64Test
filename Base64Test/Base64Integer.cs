using System.Runtime.CompilerServices;
using System.Text;

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
    private const string Base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-";

    /// <summary>
    /// Gets or sets the integer value to be converted to or from a Base64 string.
    /// </summary>
    /// <value>
    /// The integer representation of the Base64 string.
    /// </value>
    public uint IntegerValue { get; set; }

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
    internal static Base64Integer FromInteger(uint number) => new(number, ToBase64(number));

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

    /// <summary>
    /// Gets the maximum possible value of a Base64Integer.
    /// </summary>
    /// <value>
    /// The maximum possible value of a Base64Integer, represented as the maximum value of an unsigned integer (uint.MaxValue) in Base64 format.
    /// </value>
    /// <remarks>
    /// This property is useful when you need to check if a Base64Integer has reached its maximum possible value.
    /// </remarks>

    internal static Base64Integer Max => FromInteger(uint.MaxValue);
    
    private Base64Integer(uint number, string base64String)
    {
        IntegerValue = number;
        StringValue = base64String;
    }

    private static uint ToInteger(string base64)
    {
        List<char> charArray = base64
            .ToArray()
            .Reverse()
            .ToList();

        return (uint)charArray
            .Select(t => Base64Chars.IndexOf(t))
            .Select((index, i) => index * (int)Math.Pow(64, i))
            .Sum();
    }

    private static string ToBase64(uint number)
    {
        var base64Builder = new StringBuilder();
        while (number > 0)
        {
            var remainder = (int)(number % 64);
            base64Builder.Insert(0, Base64Chars[remainder]);
            number /= 64;
        }

        return base64Builder.ToString();
    }
}