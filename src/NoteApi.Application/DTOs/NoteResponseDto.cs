namespace NoteApi.Application.DTOs;

public sealed record NoteResponseDto(
    Guid Id,
    string Title,
    string Details,
    DateTimeOffset CreationDate
    );