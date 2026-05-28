using FluentValidation.TestHelper;
using NoteApi.Application.DTOs;
using NoteApi.Application.Validators;

namespace NoteApi.UnitTests;

public class NoteCreateRequestValidatorTests
{
    private readonly NoteCreateRequestValidator _validator = new();

    [Fact]
    public void Validator_WhenTitleIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var request = new NoteCreateRequestDto(Title: "", Details: "Валідні деталі замітки");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Title cannot be empty.");
    }

    [Fact]
    public void Validator_WhenRequestIsValid_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var request = new NoteCreateRequestDto(Title: "Нормальний заголовок", Details: "Нормальні деталі");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}