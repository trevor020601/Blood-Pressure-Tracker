using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}
