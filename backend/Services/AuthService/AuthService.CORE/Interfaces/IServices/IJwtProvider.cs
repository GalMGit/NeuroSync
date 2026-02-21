using AuthService.CORE.Entities;

namespace AuthService.CORE.Interfaces.IServices;

public interface IJwtProvider
{
    string GenerateToken(User user);
}