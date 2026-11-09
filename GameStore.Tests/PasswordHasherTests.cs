using GameStore.Api.Domain.Rules;

namespace GameStore.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void Verify_CorrectPassword_ReturnsTrue()
    {
        string hash = PasswordHasher.Hash("pass1234");

        Assert.True(PasswordHasher.Verify("pass1234", hash));
    }

    [Fact]
    public void Verify_WrongPassword_ReturnsFalse()
    {
        string hash = PasswordHasher.Hash("pass1234");

        Assert.False(PasswordHasher.Verify("pass12345", hash));
    }

    [Fact]
    public void Hash_SamePasswordTwice_GivesDifferentHashes()
    {
        // เพราะมี salt สุ่มทุกครั้ง -> คนที่ใช้รหัสเดียวกันจะได้ hash ไม่เหมือนกัน
        Assert.NotEqual(PasswordHasher.Hash("pass1234"), PasswordHasher.Hash("pass1234"));
    }

    [Fact]
    public void Verify_SeedHashFromSqlScript_ReturnsTrue()
    {
        // hash ของ player1 ที่อยู่ใน database/2_create_tables.sql
        const string seedHash = "PBKDF2-SHA256$100000$oSGv+WQMD9xepHHPk93Fxw==$HqGyY21Ea7RJAedvNm55z1HNYnhvV9egZN3Ts9So3Go=";

        Assert.True(PasswordHasher.Verify("pass1234", seedHash));
    }

    [Fact]
    public void Verify_MalformedHash_ReturnsFalse()
    {
        Assert.False(PasswordHasher.Verify("pass1234", "not-a-valid-hash"));
    }
}
