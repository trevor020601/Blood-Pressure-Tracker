namespace SharedLibrary.BloodPressureDomain.User;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
}
