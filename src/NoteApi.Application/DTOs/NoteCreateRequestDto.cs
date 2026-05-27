namespace NoteApi.Application.DTOs;

public sealed record NoteCreateRequestDto(
    string Title,
    string Details
    );