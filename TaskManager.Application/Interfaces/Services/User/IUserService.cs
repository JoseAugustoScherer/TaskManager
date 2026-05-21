using FluentValidation;
using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Dto.Response.User;
using TaskManager.Application.ViewModel;

namespace TaskManager.Application.Interfaces.Services.User;

public interface IUserService
{
    Task<ResponseViewModel<UserResponseDto>> CreateUserAsync(UserRequestDto requestDto, CancellationToken cancellationToken);
}