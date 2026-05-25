namespace NoteApi.Application.DTOs;

public sealed record NoteShortResponseDto(
    Guid Id,
    string Title
    );