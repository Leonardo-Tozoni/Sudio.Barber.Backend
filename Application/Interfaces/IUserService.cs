using Studio.Barber.Backend.Application.DTOs.User;

namespace Studio.Barber.Backend.Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> Create(UserCreateDTO dto);
    Task<UserDTO> Update(UserEditDTO dto);
    Task<bool> Delete(int id);
    Task<UserDTO> GetById(int id);
    Task<IEnumerable<UserDTO>> GetAll();
}