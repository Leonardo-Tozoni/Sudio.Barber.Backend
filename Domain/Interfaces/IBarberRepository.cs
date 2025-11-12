using Studio.Barber.Backend.Domain.Entities;
using BarberEntity = Studio.Barber.Backend.Domain.Entities.Barber;

namespace Studio.Barber.Backend.Domain.Interfaces;

public interface IBarberRepository
{
    Task<BarberEntity> Create(BarberEntity barber);
    Task<BarberEntity?> GetById(string id);
    Task<BarberEntity?> GetByUserId(string userId);
    Task<IEnumerable<BarberEntity>> GetAll();
}

