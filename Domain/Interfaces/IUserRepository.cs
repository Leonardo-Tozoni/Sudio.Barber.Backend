using Studio.Barber.Backend.Domain.Entities;

namespace Studio.Barber.Backend.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(string id);
    Task<User> Create(User user);
    Task<User> Update(User user);
    Task<bool> Delete(string id);
}