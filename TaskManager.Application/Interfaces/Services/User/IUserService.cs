using FluentValidation;
using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Dto.Response.User;
using TaskManager.Application.ViewModel;

namespace TaskManager.Application.Interfaces.Services.User;

public interface IUserService
{
    Task<ResponseViewModel<UserResponseDto>> CreateUserAsync(UserRequestDto requestDto, CancellationToken cancellationToken);
    Task<ResponseViewModel<UserResponseDto?>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseViewModel<UserResponseDto?>> UpdateUserAsync(Guid userId, UpdateUserRequestDto requestDto, CancellationToken cancellationToken);
    Task<ResponseViewModel<UserResponseDto?>> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseViewModel<UserResponseDto?>> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken);
    Task<ResponseViewModel<IEnumerable<UserResponseDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
}