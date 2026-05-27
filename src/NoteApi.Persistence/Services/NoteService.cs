using ErrorOr;
using Microsoft.EntityFrameworkCore;
using NoteApi.Application.DTOs;
using NoteApi.Application.Interfaces;
using NoteApi.Application.Mappings;
using NoteApi.Domain;
using NoteApi.Persistence.Context;

namespace NoteApi.Persistence.Services;

public class NoteService(NotesDbContext db) : INoteService
{
    public async Task<ErrorOr<IEnumerable<NoteShortResponseDto>>> GetAllAsync()
    {
        var notes = await db.Notes.AsNoTracking().ToListAsync();
        
        return notes.Select(n => n.ToShortDto()).ToList();
    }

    public async Task<ErrorOr<NoteResponseDto?>> GetByIdAsync(Guid id)
    {
        var note = await db.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);

        if (note is null)
            return Error.NotFound(code: "Note.NotFound", description: $"Note with ID {id} was not found.");
        
        return note.ToDto();
    }

    public async Task<ErrorOr<NoteResponseDto>> CreateAsync(NoteCreateRequestDto dto)
    {
        var note = Note.CreateNote(dto.Title, dto.Details);
        
        await db.Notes.AddAsync(note);
        await db.SaveChangesAsync();
        
        return note.ToDto();
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(Guid id, NoteUpdateRequestDto dto)
    {
        var note = await db.Notes.FindAsync(id);
        
        if(note is null)
            return Error.NotFound(code: "Note.NotFound", description: $"Note with ID {id} was not found.");
        
        note.UpdateNote(dto.Title, dto.Details);
        
        await db.SaveChangesAsync();

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id)
    {
        var note = await db.Notes.FindAsync(id);
        
        if (note is null) 
            return Error.NotFound(code: "Note.NotFound", description: $"Note with ID {id} was not found.");
        
        db.Notes.Remove(note);
        
        await db.SaveChangesAsync();

        return Result.Deleted;
    }   
}