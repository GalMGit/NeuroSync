using AuthService.CORE.Entities;

namespace AuthService.CORE.Interfaces.IServices.IAuth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}