using FluentValidation;
using NoteApi.Application.DTOs;

namespace NoteApi.Application.Validators;

public class NoteCreateRequestValidator : AbstractValidator<NoteCreateRequestDto>
{
    public NoteCreateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Details)
            .NotEmpty().WithMessage("Details cannot be empty.")
            .MaximumLength(2000).WithMessage("Details cannot exceed 2000 characters.");
    }
}