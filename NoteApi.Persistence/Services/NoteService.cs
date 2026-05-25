using Microsoft.EntityFrameworkCore;
using NoteApi.Application.DTOs;
using NoteApi.Application.Interfaces;
using NoteApi.Application.Mappings;
using NoteApi.Domain;
using NoteApi.Persistence.Context;

namespace NoteApi.Persistence.Services;

public class NoteService(NotesDbContext db) : INoteService
{
    public async Task<IEnumerable<NoteShortResponseDto>> GetAllAsync()
    {
        var notes = await db.Notes.AsNoTracking().ToListAsync();
        
        return notes.Select(n => n.ToShortDto());
    }

    public async Task<NoteResponseDto?> GetByIdAsync(Guid id)
    {
        var note = await db.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        
        return note?.ToDto();
    }

    public async Task<NoteResponseDto> CreateAsync(NoteCreateRequestDto dto)
    {
        var note = Note.CreateNote(dto.Title, dto.Details);
        
        await db.Notes.AddAsync(note);
        await db.SaveChangesAsync();
        
        return note.ToDto();
    }

    public async Task UpdateAsync(Guid id, NoteUpdateRequestDto dto)
    {
        var note = await db.Notes.FindAsync(id);
        
        note?.UpdateNote(dto.Title, dto.Details);
        
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var note = await db.Notes.FindAsync(id);
        
        db.Notes.Remove(note!);
        
        await db.SaveChangesAsync();
    }   
}