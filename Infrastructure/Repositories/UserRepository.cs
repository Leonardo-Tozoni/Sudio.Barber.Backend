using Studio.Barber.Backend.Domain.Entities;
using Studio.Barber.Backend.Domain.Interfaces;
using Studio.Barber.Backend.Infrastructure.Data;

namespace Studio.Barber.Backend.Infrastructure.Repositories;

public class UserRepository :  IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> Create(User user)
    {
       _context.Users.Add(user);
       await _context.SaveChangesAsync();
       return user;
    }
    
    public async Task<User> Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }


}