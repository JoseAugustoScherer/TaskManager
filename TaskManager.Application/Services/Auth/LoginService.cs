using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Dto.Response.User;
using TaskManager.Application.Interfaces.Services.Auth;
using TaskManager.Application.Interfaces.Services.Password;
using TaskManager.Application.Interfaces.Services.User;
using TaskManager.Application.ViewModel;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services.Auth;

public class LoginService (
    ITokenService tokenService,
    IUserRepository repository,
    IHashService hashService
) : ILoginService
{
    public async Task<ResponseViewModel<LoginResponseDto?>> LoginAsync(UserLoginRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var user = await repository.FindByEmailAsync(requestDto.Email, cancellationToken);
        
        if (user is null || !hashService.Verify(requestDto.Password, user.Password))
            return ResponseViewModel<LoginResponseDto?>.Fail(
                "Wrong credentials",
                401
            );

        var token = tokenService.GenerateToken(user);

        var response = new LoginResponseDto(token);
        
        return ResponseViewModel<LoginResponseDto?>.Ok(response);
    }
}