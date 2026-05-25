using NoteApi.Application.DTOs;

namespace NoteApi.Application.Interfaces;

public interface INoteService
{
    Task<IEnumerable<NoteShortResponseDto>> GetAllAsync();
    Task<NoteResponseDto?> GetByIdAsync(Guid id);
    Task<NoteResponseDto> CreateAsync(NoteCreateRequestDto dto);
    Task UpdateAsync(Guid id, NoteUpdateRequestDto dto);
    Task DeleteAsync(Guid id);
}