namespace NoteApi.Application.DTOs;

public sealed record NoteUpdateRequestDto(
    string Title,
    string Details
    );