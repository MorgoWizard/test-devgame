using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionUtility
{
    private const string EncryptionKey = "S1O5IF+BeHn4bqDgU1d7HTIhsoMI9hnIMMBvRO0FUzE=";

    public static string Encrypt(string plainText)
    {
        byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);
        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);
        byte[] iv = new byte[16];
        byte[] cipher = new byte[16];
        Array.Copy(fullCipher, 0, iv, 0, iv.Length);
        Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);
        using var aesAlg = Aes.Create();
        using var decryptor = aesAlg.CreateDecryptor(key, iv);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}