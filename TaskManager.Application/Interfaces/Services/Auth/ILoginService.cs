using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Dto.Response.User;
using TaskManager.Application.ViewModel;

namespace TaskManager.Application.Interfaces.Services.Auth;

public interface ILoginService
{
    Task<ResponseViewModel<LoginResponseDto?>> LoginAsync(UserLoginRequestDto requestDto, CancellationToken cancellationToken);
}