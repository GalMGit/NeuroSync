namespace AuthService.CORE.Interfaces.IServices;

public interface IPasswordHasher
{
    string GenerateHash(string password);
    bool VerifyHash(string password, string hashedPassword);
}