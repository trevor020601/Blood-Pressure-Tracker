using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.PasswordHasher;

namespace SharedLibrary.BloodPressureDomain.User;

// TODO: Refactor this once I start doing a concrete CQRS implementation...

[InjectDependency(ServiceLifetime.Scoped)]
public interface ILoginUser
{
    Task<User> Handle(LoginUser.Request request, CancellationToken cancellationToken);
}

public sealed class LoginUser : ILoginUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUser(IUserRepository userRepository,
                     IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public record Request(string Email, string Password);

    public async Task<User> Handle(Request request, 
                                   CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken) ?? throw new UserNotFoundException("The user was not found");
        var verified = _passwordHasher.Verify(request.Password, user.Password);
        if (!verified)
        {
            throw new IncorrectPasswordException("The password is incorrect");
        }
        return user;
    }
}

// TODO: Move custom exceptions to separate directory? Refactor to use result pattern instead?

[Serializable]
public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException() { }
    public UserNotFoundException(string message) : base(message) { }
    public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable]
public sealed class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException() { }
    public IncorrectPasswordException(string message) : base(message) { }
    public IncorrectPasswordException(string message, Exception innerException) : base(message, innerException) { }
}
