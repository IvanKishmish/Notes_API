using NoteApi.Application.DTOs;
using ErrorOr;

namespace NoteApi.Application.Interfaces;

public interface INoteService
{
    Task<ErrorOr<IEnumerable<NoteShortResponseDto>>> GetAllAsync();
    Task<ErrorOr<NoteResponseDto?>> GetByIdAsync(Guid id);
    Task<ErrorOr<NoteResponseDto>> CreateAsync(NoteCreateRequestDto dto);
    Task<ErrorOr<Updated>> UpdateAsync(Guid id, NoteUpdateRequestDto dto);
    Task<ErrorOr<Deleted>> DeleteAsync(Guid id);
}