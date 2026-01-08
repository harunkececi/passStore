using System.Security.Cryptography;
using System.Text;

namespace PassStore.Infrastructure.Services;

public class EncryptionService
{
    private const int KeySize = 256;
    private const int IvSize = 128;
    private const int Iterations = 10000;

    public string Encrypt(string plainText, string masterPassword)
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        var salt = GenerateSalt();
        var key = DeriveKey(masterPassword, salt);

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var result = new byte[salt.Length + aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(aes.IV, 0, result, salt.Length, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, salt.Length + aes.IV.Length, cipherBytes.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText, string masterPassword)
    {
        if (string.IsNullOrEmpty(cipherText))
            return string.Empty;

        var data = Convert.FromBase64String(cipherText);
        var salt = new byte[16];
        var iv = new byte[16];
        var cipherBytes = new byte[data.Length - 32];

        Buffer.BlockCopy(data, 0, salt, 0, 16);
        Buffer.BlockCopy(data, 16, iv, 0, 16);
        Buffer.BlockCopy(data, 32, cipherBytes, 0, cipherBytes.Length);

        var key = DeriveKey(masterPassword, salt);

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    private byte[] GenerateSalt()
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    private byte[] DeriveKey(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(KeySize / 8);
    }
}
