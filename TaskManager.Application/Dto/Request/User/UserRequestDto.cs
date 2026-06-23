namespace TaskManager.Application.Dto.Request.User;

public abstract record UserRequestDto(string Name, string Email, string Password);