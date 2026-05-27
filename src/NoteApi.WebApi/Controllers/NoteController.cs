using Microsoft.AspNetCore.Mvc;
using NoteApi.Application.DTOs;
using NoteApi.Application.Interfaces;

namespace NoteApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class NoteController(INoteService service) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await service.GetAllAsync();
        
        return result.Match(
            notes => Ok(notes), 
            errors => Problem(errors) 
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await service.GetByIdAsync(id);
        
        return result.Match(
            note => Ok(note),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(NoteCreateRequestDto dto)
    {
        var result = await service.CreateAsync(dto);
        
        return result.Match(
            newNote => CreatedAtAction(nameof(GetById), new { id = newNote.Id }, newNote),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, NoteUpdateRequestDto dto)
    {
        var result = await service.UpdateAsync(id, dto);
        
        return result.Match(
            _ => NoContent(), // Нам не треба змінна успіху, просто віддаємо 204
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await service.DeleteAsync(id);
        
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}