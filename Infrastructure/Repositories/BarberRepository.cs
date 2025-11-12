using Studio.Barber.Backend.Domain.Entities;
using Studio.Barber.Backend.Domain.Interfaces;
using Studio.Barber.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberEntity = Studio.Barber.Backend.Domain.Entities.Barber;

namespace Studio.Barber.Backend.Infrastructure.Repositories;

public class BarberRepository : IBarberRepository
{
    private readonly AppDbContext _context;

    public BarberRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<BarberEntity> Create(BarberEntity barber)
    {
        _context.Barbers.Add(barber);
        await _context.SaveChangesAsync();
        return barber;
    }
    
    public async Task<BarberEntity?> GetById(string id)
    {
        return await _context.Barbers
            .Include(b => b.user)
            .Include(b => b.barbershop)
            .FirstOrDefaultAsync(b => b.id == id);
    }
    
    public async Task<BarberEntity?> GetByUserId(string userId)
    {
        return await _context.Barbers
            .Include(b => b.user)
            .Include(b => b.barbershop)
            .FirstOrDefaultAsync(b => b.userId == userId);
    }
    
    public async Task<IEnumerable<BarberEntity>> GetAll()
    {
        return await _context.Barbers
            .Include(b => b.user)
            .Include(b => b.barbershop)
            .ToListAsync();
    }
}

