using SharedLibrary.Result;
using System.Diagnostics;

namespace SharedLibrary.BloodPressureDomain.User;

public static class UserErrors
{
    public static readonly Error ExistingUser = new("Users.Create", "User with email already exists.", new StackTrace().ToString());
}
