using AuthService.CORE.Interfaces.IServices;

namespace AuthService.BLL.Services;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateHash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);
    
    public bool VerifyHash(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}