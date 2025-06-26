using System.Security.Cryptography;
using System.Text;
// ReSharper disable ArrangeObjectCreationWhenTypeEvident

namespace BloodTourney.Security;

public static class Encryption
{
    private readonly static byte[] HmacKey = Encoding.UTF8.GetBytes("OnytnBs39oyrvUr6q82ForwlNlQDrmkbUDvTmVwmW2hcWtk1/L/sNFMGeGVMAOdj"); // 32
    private readonly static byte[] Key = Encoding.UTF8.GetBytes("HAZn6Oz+T40vPOZoDCOVYIFUg8fUzHoHRWGcPQ44yUeRuQGZGJyMLNFh7+OrcDhz"); // 32  
    private readonly static byte[] IV = Encoding.UTF8.GetBytes("37a7e57e38c91846103e27e2a9941ee5");  // 16

    public static byte[] EncryptStringToFile(string stringToEncrypt)
    {
        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(stringToEncrypt);
        
        HMACSHA256 hmac = new(HmacKey);
        byte[] hmacHash = hmac.ComputeHash(bytesToEncrypt);
        
        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
        
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
        cs.FlushFinalBlock();
        
        byte[] encryptedBytes = ms.ToArray();
        byte[] encryptedWithHmac = hmacHash.Concat(encryptedBytes).ToArray();
        
        return encryptedWithHmac;
    }


    public static string DecryptFromFileToString(byte[] fileData)
    {
        // Splitting the given byte[] to fetch the HMAC from the first 32 bytes
        byte[] fileHmac = fileData.Take(32).ToArray();
        byte[] encryptedBytes = fileData.Skip(32).ToArray();
    
        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
    
        // Decrypt file contents without HMAC
        using MemoryStream ms = new(encryptedBytes);
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using MemoryStream msDecrypted = new();
    
        cs.CopyTo(msDecrypted);
        byte[] decryptedBytes = msDecrypted.ToArray();

        // Check HMAC of decrypted plaintext
        using HMACSHA256 hmac = new(HmacKey);
        byte[] computedHmac = hmac.ComputeHash(decryptedBytes);

        if (!fileHmac.SequenceEqual(computedHmac))
        {
            throw new CryptographicException("HMAC verification failed for given file data.");
        }
    
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}