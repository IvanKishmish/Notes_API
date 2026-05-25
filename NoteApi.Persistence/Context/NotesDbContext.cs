using Microsoft.EntityFrameworkCore;
using NoteApi.Domain;

namespace NoteApi.Persistence.Context;

public class NotesDbContext(DbContextOptions<NotesDbContext> options)
    : DbContext(options) 
{
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Це автоматично знайде всі класи, що реалізують IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    
}