using System.Security.Cryptography;

namespace GameStore.Api.Domain.Rules;

// ไม่เก็บรหัสผ่านจริงลง database แต่เก็บเป็น hash แบบ PBKDF2 (มี salt สุ่มแยกของแต่ละ user)
// รูปแบบที่เก็บ:  PBKDF2-SHA256$<iterations>$<salt base64>$<hash base64>
// ถ้า database หลุดออกไป ก็ยังไม่รู้รหัสผ่านจริงของผู้ใช้
public static class PasswordHasher
{
    private const string Algorithm = "PBKDF2-SHA256";
    private const int Iterations = 100_000;
    private const int SaltSize = 16;
    private const int KeySize = 32;

    public static string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        return $"{Algorithm}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
    }

    public static bool Verify(string password, string storedHash)
    {
        string[] parts = storedHash.Split('$');
        if (parts.Length != 4 || parts[0] != Algorithm || !int.TryParse(parts[1], out int iterations))
            return false;

        byte[] salt = Convert.FromBase64String(parts[2]);
        byte[] expected = Convert.FromBase64String(parts[3]);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);

        // เทียบแบบใช้เวลาคงที่ ป้องกัน timing attack
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
