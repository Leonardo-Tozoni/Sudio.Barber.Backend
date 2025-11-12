using Studio.Barber.Backend.Application.DTOs.BarberDTO;
using Studio.Barber.Backend.Application.Interfaces;
using Studio.Barber.Backend.Domain.Entities;
using Studio.Barber.Backend.Domain.Interfaces;
using Studio.Barber.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberEntity = Studio.Barber.Backend.Domain.Entities.Barber;

namespace Studio.Barber.Backend.Application.Services;

public class BarberService : IBarberService
{
    private readonly IBarberRepository _barberRepository;
    private readonly IUserRepository _userRepository;
    private readonly AppDbContext _context;

    public BarberService(IBarberRepository barberRepository, IUserRepository userRepository, AppDbContext context)
    {
        _barberRepository = barberRepository;
        _userRepository = userRepository;
        _context = context;
    }
    
    public async Task<BarberDTO> Create(BarberCreateDTO dto)
    {
        // Verificar se a barbearia existe (apenas se barbershopId foi fornecido)
        if (!string.IsNullOrEmpty(dto.BarbershopId))
        {
            var barbershop = await _context.Barbershops.FirstOrDefaultAsync(bs => bs.id == dto.BarbershopId);
            if (barbershop == null)
            {
                throw new ArgumentException($"Barbearia com ID '{dto.BarbershopId}' não encontrada. Verifique se o ID está correto e se a barbearia existe no banco de dados.");
            }
        }

        User user;
        
        // Se UserId foi fornecido, busca usuário existente
        if (!string.IsNullOrEmpty(dto.UserId))
        {
            var existingUser = await _userRepository.GetUserById(dto.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"Usuário com ID '{dto.UserId}' não encontrado. Verifique se o ID está correto e se o usuário existe no banco de dados.");
            }
            user = existingUser;
        }
        else
        {
            // Cria novo usuário
            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new ArgumentException("Email é obrigatório para criar um novo usuário.");
            }

            // Verifica se já existe usuário com este email
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Já existe um usuário com o email '{dto.Email}'. Use o UserId existente ou escolha outro email.");
            }

            user = new User
            {
                name = dto.Name,
                email = dto.Email,
                role = dto.Role ?? "BARBER"
            };

            user = await _userRepository.Create(user);
        }

        // Verifica se o usuário já é um barbeiro
        var existingBarber = await _barberRepository.GetByUserId(user.id);
        if (existingBarber is not null)
        {
            throw new InvalidOperationException($"Este usuário (ID: '{user.id}') já é um barbeiro.");
        }

        // Cria o barbeiro
        var barber = new BarberEntity
        {
            userId = user.id,
            barbershopId = dto.BarbershopId,
            createdAt = DateTime.UtcNow
        };
        
        var result = await _barberRepository.Create(barber);
        
        return new BarberDTO
        {
            Id = result.id,
            UserId = result.userId,
            BarbershopId = result.barbershopId,
            CreatedAt = result.createdAt
        };
    }
    
    public async Task<BarberDTO?> GetById(string id)
    {
        var barber = await _barberRepository.GetById(id);
        if (barber is null) return null;
        
        return new BarberDTO
        {
            Id = barber.id,
            UserId = barber.userId,
            BarbershopId = barber.barbershopId,
            CreatedAt = barber.createdAt
        };
    }
    
    public async Task<BarberDTO?> GetByUserId(string userId)
    {
        var barber = await _barberRepository.GetByUserId(userId);
        if (barber is null) return null;
        
        return new BarberDTO
        {
            Id = barber.id,
            UserId = barber.userId,
            BarbershopId = barber.barbershopId,
            CreatedAt = barber.createdAt
        };
    }
    
    public async Task<IEnumerable<BarberDTO>> GetAll()
    {
        var barbers = await _barberRepository.GetAll();
        
        return barbers.Select(b => new BarberDTO
        {
            Id = b.id,
            UserId = b.userId,
            BarbershopId = b.barbershopId,
            CreatedAt = b.createdAt
        });
    }
}

