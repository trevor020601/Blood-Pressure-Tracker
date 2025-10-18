namespace Tests.PasswordHasher;

public class PasswordHasherTest
{
    [Fact]
    public void ShouldHashPasswordAndVerify()
    {
        var password = "password123";
        var passwordHasher = new SharedLibrary.PasswordHasher.PasswordHasher();
        var passwordHash = passwordHasher.Hash(password);
        var isVerified = passwordHasher.Verify(password, passwordHash);
        Assert.True(isVerified);
    }
}
