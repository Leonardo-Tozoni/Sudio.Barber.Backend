using Microsoft.AspNetCore.Mvc;
using Studio.Barber.Backend.Application.DTOs.User;
using Studio.Barber.Backend.Application.Interfaces;

namespace Studio.Barber.Backend.Controller;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
    private readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create([FromBody] UserCreateDTO dto)
    {
        var resultado = await _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDTO>> Atualizar(string id, [FromBody] UserEditDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("ID da URL não corresponde ao ID do body");
        
        var resultado = await _service.Update(dto);
        if (resultado == null)
            return NotFound();
        
        return Ok(resultado);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var sucesso = await _service.Delete(id);
        if (!sucesso)
            return NotFound();
        
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(string id)
    {
        var usuario = await _service.GetById(id);
        if (usuario == null)
            return NotFound();
        
        return Ok(usuario);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
    {
        var usuarios = await _service.GetAll();
        return Ok(usuarios);
    }
}