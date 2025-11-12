using Studio.Barber.Backend.Application.DTOs.User;
using Studio.Barber.Backend.Application.Interfaces;
using Studio.Barber.Backend.Domain.Entities;
using Studio.Barber.Backend.Domain.Interfaces;

namespace Studio.Barber.Backend.Application.Services;

public class UserService :  IUserService
{
       private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<UserDTO> Create(UserCreateDTO dto)
    {
        var usuario = new User
        {
            name = dto.Name,
            email = dto.Email,
            role = dto.Role
        };
        
        var result = await _repository.Create(usuario);
        
        return new UserDTO
        {
            Id = result.id,
            Name = result.name,
            Email = result.email,
            Role = result.role
        };
    }
    
    public async Task<UserDTO> Update(UserEditDTO dto)
    {
        var user = await _repository.Update(new User());
        if (user == null) return null;
        
        user.name = dto.Name;
        user.email = dto.Email;
        
        var resultado = await _repository.Update(user);
        
        return new UserDTO()
        {
            Id = resultado.id,
            Name = resultado.name,
            Email = resultado.email
        };
    }
    
    public async Task<bool> Delete(string id)
    {
        return await _repository.Delete(id);
    }
    
    public async Task<UserDTO> GetById(string id)
    {
        var user = await _repository.GetUserById(id);
        if (user == null) return null;
        
        return new UserDTO()
        {
            Id = user.id,
            Name = user.name,
            Email = user.email
        };
    }
    
    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var user = await _repository.GetAllUsers();
        
        return user.Select(u => new UserDTO()
        {
            Id = u.id,
            Name = u.name,
            Email = u.email
        });
    }
}