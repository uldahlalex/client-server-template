using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace Service.Security;

public class Argon2idPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    private const string Name = "argon2id";

    public string HashPassword(TUser user, string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);
        var hash = GenerateHash(password, salt);
        return $"{Name}${Encode(salt)}${Encode(hash)}";
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        var parts = hashedPassword.Split('$');
        var salt = Decode(parts[1]);
        var storedHash = Decode(parts[2]);
        var providedHash = GenerateHash(providedPassword, salt);
        return ByteArraysEqual(storedHash, providedHash)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }

    public byte[] GenerateHash(string password, byte[] salt)
    {
        using var hashAlgo = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            MemorySize = 12288,
            Iterations = 3,
            DegreeOfParallelism = 1
        };
        return hashAlgo.GetBytes(256 / 8);
    }

    protected byte[] Decode(string value)
    {
        return Convert.FromBase64String(value);
    }

    protected string Encode(byte[] value)
    {
        return Convert.ToBase64String(value);
    }

    // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
    // From: https://github.com/aspnet/AspNetIdentity/blob/main/src/Microsoft.AspNet.Identity.Core/Crypto.cs
    [MethodImpl(MethodImplOptions.NoOptimization)]
    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;
        var areSame = true;
        for (var i = 0; i < a.Length; i++) areSame &= a[i] == b[i];
        return areSame;
    }
}