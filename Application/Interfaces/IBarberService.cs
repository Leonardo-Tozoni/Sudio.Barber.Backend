using Studio.Barber.Backend.Application.DTOs.BarberDTO;

namespace Studio.Barber.Backend.Application.Interfaces;

public interface IBarberService
{
    Task<BarberDTO> Create(BarberCreateDTO dto);
    Task<BarberDTO?> GetById(string id);
    Task<BarberDTO?> GetByUserId(string userId);
    Task<IEnumerable<BarberDTO>> GetAll();
}

