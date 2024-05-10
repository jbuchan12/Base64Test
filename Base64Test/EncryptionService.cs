namespace Base64Test;

/// <summary>
/// Provides methods for encrypting and decrypting <see cref="Subscription"/> instances.
/// </summary>
/// <remarks>
/// This class uses AES encryption for the encryption and decryption of <see cref="Subscription"/> instances.
/// The encryption key is a tuple of two strings.
/// </remarks>
internal class EncryptionService
{
    private readonly AesEncryptionService _aesEncryptionService = new ();
    private readonly (string, string) _encryptionKey = new("", "");

    /// <summary>
    /// Encrypts the given <see cref="Subscription"/> instance into a string.
    /// </summary>
    /// <param name="subscription">The <see cref="Subscription"/> instance to be encrypted.</param>
    /// <returns>A string representing the encrypted subscription.</returns>
    /// <remarks>
    /// This method uses the AES encryption service to encrypt the subscription. 
    /// The subscription is first converted to a string using its ToString method, 
    /// and then encrypted using the encryption key stored in the EncryptionService instance.
    /// </remarks>

    public string EncryptSubscription(Subscription subscription) 
        => _aesEncryptionService.EncryptString(subscription.ToString(), _encryptionKey);

    /// <summary>
    /// Decrypts the provided encrypted string to a <see cref="Subscription"/> instance.
    /// </summary>
    /// <param name="encryptedString">The encrypted string representing a <see cref="Subscription"/> instance.</param>
    /// <returns>A <see cref="Subscription"/> instance that represents the decrypted string.</returns>
    /// <remarks>
    /// This method uses the AES encryption service to decrypt the string. 
    /// The decryption key is a tuple of two strings.
    /// </remarks>
    /// <exception cref="InvalidSubscriptionDataFormatException">
    /// Thrown when the decrypted subscription data does not contain at least 2 data columns or when the DateTime component of the subscription string is invalid.
    /// </exception>

    public Subscription DecryptSubscription(string encryptedString)
    {
        string decryptedString = _aesEncryptionService.DecryptString(encryptedString, _encryptionKey);
        return new Subscription(decryptedString);
    }
}