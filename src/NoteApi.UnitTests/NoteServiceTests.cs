using ErrorOr;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NoteApi.Domain;
using NoteApi.Persistence.Context;
using NoteApi.Persistence.Services;

namespace NoteApi.UnitTests;

public class NoteServiceTests
{
    private static NotesDbContext CreateDbContext()
    {
        // Хелпер-метод для створення чистого DbContext в оперативній пам'яті.
        // Щоразу створюється нова база даних, щоб тести не заважали один одному.
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.CreateVersion7().ToString())
            .Options;
        
        return new NotesDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_WhenNoteExists_ShouldReturnNoteDto()
    {
        // 1. Arrange (Підготовка: створюємо базу і додаємо туди фейкову нотатку)
        await using var context = CreateDbContext();
        
        var existingNote = Note.CreateNote("Мій заголовок", "Якісь деталі");
        context.Notes.Add(existingNote);
        await context.SaveChangesAsync();

        var service = new NoteService(context);
        
        // 2. Act (Дія: викликаємо метод, який тестуємо)
        var result = await service.GetByIdAsync(existingNote.Id);
        
        // 3. Assert (Перевірка результату за допомогою FluentAssertions)
        result.IsError.Should().BeFalse(); // Помилки бути не повинно
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(existingNote.Id);
        result.Value.Title.Should().Be("Мій заголовок");
        result.Value.Details.Should().Be("Якісь деталі");
    }
    
    [Fact]
    public async Task GetByIdAsync_WhenNoteDoesNotExist_ShouldReturnNotFound()
    {
        // 1. Arrange (База порожня)
        await using var context = CreateDbContext();
        var service = new NoteService(context);
        var nonExistingId = Guid.NewGuid();

        // 2. Act
        var result = await service.GetByIdAsync(nonExistingId);

        // 3. Assert (Перевіряємо, чи повернулася правильна помилка через ErrorOr)
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
        result.FirstError.Code.Should().Be("Note.NotFound");
    }
}