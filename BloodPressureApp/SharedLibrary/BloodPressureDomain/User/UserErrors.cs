using SharedLibrary.Result;
using System.Diagnostics;

namespace SharedLibrary.BloodPressureDomain.User;

public static class UserErrors
{
    public static readonly Error ExistingUser = new("Users.Create", "User with email already exists.", new StackTrace().ToString());
    public static readonly Error UserDoesNotExist = new("User.Delete", "User with email does not exist.", new StackTrace().ToString());
    public static readonly Error IncorrectPassword = new("User.Retrieve", "Incorrect password.", new StackTrace().ToString());
    public static readonly Error ExpiredRefreshToken = new("User.LoginRefreshToken", "The refresh token has expired.", new StackTrace().ToString());
}
