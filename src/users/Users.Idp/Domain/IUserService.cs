namespace Users.Idp.Domain;

public interface IUserService
{
    Task<User> GetUserAsync(Guid id);
}
