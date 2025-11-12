using Microsoft.AspNetCore.Mvc;
using Studio.Barber.Backend.Application.DTOs.BarberDTO;
using Studio.Barber.Backend.Application.Interfaces;

namespace Studio.Barber.Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class BarberController : ControllerBase
{
    private readonly IBarberService _service;
    
    public BarberController(IBarberService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<ActionResult<BarberDTO>> Create([FromBody] BarberCreateDTO dto)
    {
        try
        {
            var resultado = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao criar barbeiro", error = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BarberDTO>> GetById(string id)
    {
        var barber = await _service.GetById(id);
        if (barber is null)
            return NotFound();
        
        return Ok(barber);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<BarberDTO>> GetByUserId(string userId)
    {
        var barber = await _service.GetByUserId(userId);
        if (barber is null)
            return NotFound();
        
        return Ok(barber);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BarberDTO>>> GetAll()
    {
        var barbers = await _service.GetAll();
        return Ok(barbers);
    }
}

