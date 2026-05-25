using NoteApi.Application.DTOs;
using NoteApi.Domain;

namespace NoteApi.Application.Mappings;

public static class NoteMappingExtensions
{
    public static NoteResponseDto ToDto(this Note note)
    {
        return new NoteResponseDto(
            note.Id,
            note.Title,
            note.Details,
            note.CreationDate
        );
    }
    
    public static NoteShortResponseDto ToShortDto(this Note note)
    {
        return new NoteShortResponseDto(
            note.Id,
            note.Title
        );
    }
}