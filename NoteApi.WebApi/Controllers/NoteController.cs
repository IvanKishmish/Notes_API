using Microsoft.AspNetCore.Mvc;
using NoteApi.Application.DTOs;
using NoteApi.Application.Interfaces;

namespace NoteApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class NoteController(INoteService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteShortResponseDto>>> GetAll()
    {
        var notes = await service.GetAllAsync();
        
        return Ok(notes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NoteResponseDto>> GetById(Guid id)
    {
        var note = await service.GetByIdAsync(id);
        
        if(note is null) return NotFound();
        
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NoteCreateRequestDto dto)
    {
        var newNote = await service.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = newNote.Id }, newNote);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, NoteUpdateRequestDto dto)
    {
        await service.UpdateAsync(id, dto);
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeleteAsync(id);
        
        return NoContent();
    }
}