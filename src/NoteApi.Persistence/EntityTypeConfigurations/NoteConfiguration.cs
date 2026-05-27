using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApi.Domain;

namespace NoteApi.Persistence.EntityTypeConfigurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
        
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(n => n.Details)
            .IsRequired()
            .HasMaxLength(2000);
    }
}