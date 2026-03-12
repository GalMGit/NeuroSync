using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;
using UserService.CORE.Interfaces.IServices;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserGetController(
    IUserService userService
    ) : BaseController
{
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        try
        {
            var profile = await userService
                .GetUserProfileAsync(UserId);

            return Ok(profile);
        }
        catch (UnauthorizedAccessException)
        {
            return Problem(
                title: "Не авторизован",
                detail: "Требуется авторизация для доступа к профилю",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }
        catch (Exception e)
        {
            return Problem(
                title: "Ошибка получения профиля",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserProfile(Guid userId)
    {
        try
        {
            var profile = await userService
                .GetUserProfileAsync(userId);

            return Ok(profile);
        }
        catch (UnauthorizedAccessException)
        {
            return Problem(
                title: "Доступ запрещен",
                detail: "У вас нет прав на просмотр этого профиля",
                statusCode: StatusCodes.Status403Forbidden
            );
        }
        catch (Exception e)
        {
            var (statusCode, title) = e.Message switch
            {
                var msg when msg.Contains("удален") => (
                    StatusCodes.Status404NotFound,
                    "Профиль удален"
                ),
                var msg when msg.Contains("не найден") => (
                    StatusCodes.Status404NotFound,
                    "Профиль не найден"
                ),
                _ => (
                    StatusCodes.Status400BadRequest,
                    "Ошибка получения профиля"
                )
            };

            return Problem(
                title: title,
                detail: e.Message,
                statusCode: statusCode
            );
        }
    }
}