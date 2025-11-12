using Studio.Barber.Backend.Domain.Entities;
using Studio.Barber.Backend.Domain.Interfaces;
using Studio.Barber.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        // Sempre usar SQL direto para fazer cast do role para enum UserRole
        var roleValue = !string.IsNullOrEmpty(user.role) ? user.role.ToUpper() : null;
        
        if (roleValue != null)
        {
            // Com role - usar SQL raw com cast explícito
            await _context.Database.ExecuteSqlRawAsync(
                @"INSERT INTO ""User"" (id, name, email, role, ""emailVerified"", image, phone, ""cookieConsent"", ""cookieConsentDate"")
                  VALUES ({0}, {1}, {2}, {3}::""UserRole"", {4}, {5}, {6}, {7}, {8})",
                user.id ?? string.Empty,
                user.name ?? (object?)null,
                user.email ?? (object?)null,
                roleValue,
                user.emailVerified ?? (object?)null,
                user.image ?? (object?)null,
                user.phone ?? (object?)null,
                user.cookieConsent ?? (object?)null,
                user.cookieConsentDate ?? (object?)null);
        }
        else
        {
            // Sem role - inserir NULL
            await _context.Database.ExecuteSqlRawAsync(
                @"INSERT INTO ""User"" (id, name, email, role, ""emailVerified"", image, phone, ""cookieConsent"", ""cookieConsentDate"")
                  VALUES ({0}, {1}, {2}, NULL, {3}, {4}, {5}, {6}, {7})",
                user.id ?? string.Empty,
                user.name ?? (object?)null,
                user.email ?? (object?)null,
                user.emailVerified ?? (object?)null,
                user.image ?? (object?)null,
                user.phone ?? (object?)null,
                user.cookieConsent ?? (object?)null,
                user.cookieConsentDate ?? (object?)null);
        }
        
        return user;
    }
    
    public async Task<User> Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> Delete(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserById(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.id == id);
    }


}