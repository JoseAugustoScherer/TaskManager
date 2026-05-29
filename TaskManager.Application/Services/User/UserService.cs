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
    public async Task<ResponseViewModel<CreateUserResponseDto>> CreateUserAsync(UserRequestDto requestDto, CancellationToken cancellationToken)
    {
        var validationResult = await new UserCreateRequestDtoValidation().ValidateAsync(requestDto, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            return ResponseViewModel<CreateUserResponseDto>.Fail(
                errors,
                400
            );
        }

        var user = await repository.FindByEmailAsync(requestDto.Email, cancellationToken);

        if (user is not null)
            return ResponseViewModel<CreateUserResponseDto>.Fail(
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
        
        var response = new CreateUserResponseDto(newUser.Id, newUser.Name, newUser.Email);

        return ResponseViewModel<CreateUserResponseDto>.Ok(response);
    }

    public async Task<ResponseViewModel<CreateUserResponseDto?>> GetUserByIdAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(userId, cancellationToken);
        
        if(user is null)
            return ResponseViewModel<CreateUserResponseDto?>.Fail(
                $"User with ID {userId} don't exists",
                404
            );

        var response = new CreateUserResponseDto(user.Id, user.Name, user.Email);
        
        return ResponseViewModel<CreateUserResponseDto?>.Ok(response);
    }

    public async Task<ResponseViewModel<UpdateUserResponseDto>> UpdateUserAsync(Guid userId, UpdateUserRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await new UserUpdateRequestDtoValidation().ValidateAsync(requestDto, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            return ResponseViewModel<UpdateUserResponseDto>.Fail(
                errors,
                400
            );
        }
        
        if (requestDto.Name is null && requestDto.Email is null)                                                                                                                                                                          
            return ResponseViewModel<UpdateUserResponseDto>.Fail(
                "At least one field must be provided",
                400
            );
        
        var user = await repository.GetByIdAsync(userId, cancellationToken);
        
        if(user is null)
            return ResponseViewModel<UpdateUserResponseDto>.Fail(
                $"User with ID {userId} don't exists",
                404
            );
        
        if (requestDto.Email is not null)                                                                                                                                                                                                 
        {                                                                                                                                                                                                                               
            var email = await repository.FindByEmailAsync(requestDto.Email, cancellationToken);                                                                                                                                           
            if (email is not null && email.Id != userId)                                                                                                                                                                                  
                return ResponseViewModel<UpdateUserResponseDto>.Fail("Email already exists", 409);
        } 

        if (requestDto.Name is not null)
            user.UpdateName(requestDto.Name);
        if (requestDto.Email is not null)
            user.UpdateEmail(requestDto.Email);
        
        await repository.UpdateAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return ResponseViewModel<UpdateUserResponseDto>.Ok(
            new UpdateUserResponseDto(
                Name: user.Name,
                Email: user.Email
            )
        );
    }

    public Task<ResponseViewModel<CreateUserResponseDto?>> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseViewModel<UserResponseDto?>> GetUserByEmailAsync(string userEmail,
        CancellationToken cancellationToken)
    {
        var user = await repository.FindByEmailAsync(userEmail, cancellationToken);
        if (user is null)
            return ResponseViewModel<UserResponseDto?>.Fail(
                "User not found",
                404
            );
        
        var response = new UserResponseDto(user.Id, user.Name, user.Email);
        
        return ResponseViewModel<UserResponseDto?>.Ok(response);
    }

    public async Task<ResponseViewModel<IEnumerable<UserResponseDto>>> GetAllUsersAsync(
        CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        
        var userDto = users.Select(u => new UserResponseDto(u.Id, u.Name, u.Email)).ToList();
        
        return ResponseViewModel<IEnumerable<UserResponseDto>>.Ok(userDto);
    }
}