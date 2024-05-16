using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Base64Test.Test")]
namespace Base64Test;

/// <summary>
/// Represents a subscription with a Base64 encoded integer ID and an expiry date.
/// </summary>
/// <remarks>
/// This class provides methods to create a subscription from a Base64 encoded string.
/// The string should contain two data columns separated by a "|". The first column is the Base64 encoded integer ID and the second column is the expiry date in ticks.
/// </remarks>
internal class Subscription
{
    private readonly string _rawString;
    
    /// <summary>
    /// Gets or sets the Base64 encoded integer ID of the subscription.
    /// </summary>
    /// <value>
    /// The Base64 encoded integer ID of the subscription.
    /// </value>
    /// <remarks>
    /// This property represents the unique identifier for a subscription, encoded as a Base64 string.
    /// </remarks>

    internal Base64Integer Id { get; set; }
    /// <summary>
    /// Gets or sets the expiry date of the subscription.
    /// </summary>
    /// <value>
    /// The expiry date of the subscription.
    /// </value>
    /// <remarks>
    /// This property represents the date and time when the subscription expires. 
    /// It is set during the initialization of the <see cref="Subscription"/> object from a Base64 encoded string.
    /// </remarks>

    internal DateTime Expiry { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Subscription"/> class.
    /// </summary>
    /// <param name="base64Subscription">The Base64 encoded subscription string.</param>
    /// <remarks>
    /// The input string should contain two data columns separated by a "|". 
    /// The first column is the Base64 encoded integer ID and the second column is the expiry date in ticks.
    /// </remarks>
    /// <exception cref="InvalidSubscriptionDataFormatException">
    /// Thrown when the subscription data does not contain at least 2 data columns or when the DateTime component of the subscription string is invalid.
    /// </exception>

    internal Subscription(string base64Subscription)
    {
        _rawString = base64Subscription;
        
        string[] dataColumns = base64Subscription.Split("|");

        if (dataColumns.Length != 2)
            throw new InvalidSubscriptionDataFormatException("Subscription data requires at least 2 data columns");

        Id = Base64Integer.FromString(dataColumns[0]);

        if (!long.TryParse(dataColumns[1], out long dateTimeTicks))
            throw new InvalidSubscriptionDataFormatException("DateTime component of the subscription string is invalid");

        try
        {
            Expiry = new DateTime(dateTimeTicks);
        }
        catch
        {
            throw new InvalidSubscriptionDataFormatException(
                "The Number of ticks in the datetime component were out of range, this number needs to be positive and lower than the maximum ticks");
        }
    }

    public override string ToString() => _rawString;
}

/// <summary>
/// The exception that is thrown when the subscription data format is invalid.
/// </summary>
/// <remarks>
/// This exception is thrown when the subscription data does not contain at least 2 data columns or when the DateTime component of the subscription string is invalid.
/// </remarks>
internal class InvalidSubscriptionDataFormatException(string message) : Exception(message);