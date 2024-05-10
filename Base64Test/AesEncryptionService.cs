using System.Security.Cryptography;

namespace Base64Test;

public interface IAesEncryptionService
{
    string EncryptString(string plainText, (string key, string iv) key);
    string DecryptString(string encryptedValue, (string key, string iv) key);
}

/// <summary>
/// Provides methods for AES encryption and decryption.
/// </summary>
/// <remarks>
/// This class implements the IAesEncryptionService interface and provides methods for encrypting and decrypting strings using the AES algorithm.
/// The encryption and decryption keys are tuples of two strings.
/// </remarks>
public class AesEncryptionService : IAesEncryptionService
{
    /// <summary>
    /// Encrypts the specified plain text using the AES algorithm.
    /// </summary>
    /// <param name="plainText">The text to be encrypted.</param>
    /// <param name="key">The encryption key and initialization vector (IV) used for the AES algorithm.</param>
    /// <returns>The encrypted text, represented as a Base64 string.</returns>
    /// <remarks>
    /// This method uses the AES symmetric algorithm for encryption. The key and IV are provided as a tuple of two strings.
    /// The method creates an AES encryptor using the provided key and IV, and then writes the plain text into a CryptoStream object.
    /// The encrypted text is then converted into a Base64 string before it is returned.
    /// </remarks>

    public string EncryptString(string plainText, (string key, string iv) key)
    {
        using Aes aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key.key);
        aes.IV = Convert.FromBase64String(key.iv);

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    /// <summary>
    /// Decrypts the specified encrypted text using the AES algorithm.
    /// </summary>
    /// <param name="encryptedValue">The text to be decrypted, represented as a Base64 string.</param>
    /// <param name="key">The decryption key and initialization vector (IV) used for the AES algorithm.</param>
    /// <returns>The decrypted text.</returns>
    /// <remarks>
    /// This method uses the AES symmetric algorithm for decryption. The key and IV are provided as a tuple of two strings.
    /// The method creates an AES decryptor using the provided key and IV, and then reads the encrypted text from a CryptoStream object.
    /// The decrypted text is then returned.
    /// </remarks>

    public string DecryptString(string encryptedValue, (string key, string iv) key)
    {
        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = Convert.FromBase64String(key.key);
        aes.IV = Convert.FromBase64String(key.iv);

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        byte[] encryptedArray = Convert.FromBase64String(encryptedValue);

        using var msDecrypt = new MemoryStream(encryptedArray);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }

    /// <summary>
    /// Generates a new encryption key and initialization vector (IV) for the AES algorithm.
    /// </summary>
    /// <returns>A tuple containing the encryption key and IV, both represented as Base64 strings.</returns>
    /// <remarks>
    /// This method creates a new instance of the AES algorithm with a key size of 256 bits. 
    /// It then generates a new random key and IV using the AES instance, and returns them as a tuple of two Base64 strings.
    /// </remarks>

    public (string key, string iv) GenerateKey()
    {
        using Aes aes256 = Aes.Create();
        aes256.KeySize = 256;

        return (Convert.ToBase64String(aes256.Key), Convert.ToBase64String(aes256.IV));
    }
}

