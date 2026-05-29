using FluentValidation;
using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Dto.Response.User;
using TaskManager.Application.ViewModel;

namespace TaskManager.Application.Interfaces.Services.User;

public interface IUserService
{
    Task<ResponseViewModel<CreateUserResponseDto>> CreateUserAsync(UserRequestDto requestDto, CancellationToken cancellationToken);
    Task<ResponseViewModel<CreateUserResponseDto?>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseViewModel<UpdateUserResponseDto>> UpdateUserAsync(Guid userId, UpdateUserRequestDto requestDto, CancellationToken cancellationToken);
    Task<ResponseViewModel<CreateUserResponseDto?>> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseViewModel<CreateUserResponseDto?>> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken);
    Task<ResponseViewModel<IEnumerable<CreateUserResponseDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
}