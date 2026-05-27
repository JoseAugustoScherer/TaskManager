using Microsoft.Extensions.Logging;
using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Dto.Response.User;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Interfaces.Services.User;
using TaskManager.Application.Validators.User;
using TaskManager.Application.ViewModel;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services.User;

using User =  TaskManager.Domain.Entities.User;

public class UserService(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IHashService hashService
) : IUserService
{
    public async Task<ResponseViewModel<UserResponseDto>> CreateUserAsync(UserRequestDto requestDto, CancellationToken cancellationToken)
    {
        var validationResult = await new UserRequestDtoValidation().ValidateAsync(requestDto, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            return ResponseViewModel<UserResponseDto>.Fail(
                errors,
                400
            );
        }

        var user = await repository.FindByEmailAsync(requestDto.Email, cancellationToken);

        if (user is not null)
            return ResponseViewModel<UserResponseDto>.Fail(
                "User already exists",
                409
            );

        var passwordHash = hashService.Hash(requestDto.Password);

        var newUser = User.Create(
            requestDto.Name,
            requestDto.Email,
            passwordHash);

        await repository.CreateAsync(newUser, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        
        var response = new UserResponseDto(newUser.Id, newUser.Name, newUser.Email);

        return ResponseViewModel<UserResponseDto>.Ok(response);
    }

    public async Task<ResponseViewModel<UserResponseDto?>> GetUserByIdAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(userId, cancellationToken);
        
        if(user is null)
            return ResponseViewModel<UserResponseDto?>.Fail(
                $"User with ID {userId} don't exists",
                404
            );

        var response = new UserResponseDto(user.Id, user.Name, user.Email);
        
        return ResponseViewModel<UserResponseDto?>.Ok(response);
    }

    public Task<ResponseViewModel<UserResponseDto?>> UpdateUserAsync(Guid userId, UpdateUserRequestDto request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<UserResponseDto?>> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<UserResponseDto?>> GetUserByEmailAsync(string userEmail,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseViewModel<IEnumerable<UserResponseDto>>> GetAllUsersAsync(
        CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        
        var userDto = users.Select(u => new UserResponseDto(u.Id, u.Name, u.Email)).ToList();
        
        return ResponseViewModel<IEnumerable<UserResponseDto>>.Ok(userDto);
    }
}