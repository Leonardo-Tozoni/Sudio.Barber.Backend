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
            password = dto.Password,
            role = dto.Role,
            createdAt = DateTime.UtcNow
        };
        
        var result = await _repository.Create(usuario);
        
        return new UserDTO
        {
            Id = result.id,
            Name = result.name,
            Email = result.email,
            Password = result.password,
            Role = result.role,
            CreatedAt = result.createdAt
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
            Email = resultado.email,
            CreatedAt = resultado.createdAt
        };
    }
    
    public async Task<bool> Delete(int id)
    {
        return await _repository.Delete(id);
    }
    
    public async Task<UserDTO> GetById(int id)
    {
        var user = await _repository.GetUserById(id);
        if (user == null) return null;
        
        return new UserDTO()
        {
            Id = user.id,
            Name = user.name,
            Email = user.email,
            CreatedAt = user.createdAt
        };
    }
    
    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var user = await _repository.GetAllUsers();
        
        return user.Select(u => new UserDTO()
        {
            Id = u.id,
            Name = u.name,
            Email = u.email,
            CreatedAt = u.createdAt
        });
    }
}