using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApi.Application.DTOs;
using NoteApi.Application.Interfaces;
using NoteApi.Persistence.Context;
using NoteApi.Persistence.Services;

namespace NoteApi.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Налаштовуємо підключення до PostgreSQL
        services.AddDbContext<NotesDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DB_CONNECTION_STRING")));

        // Реєструємо наш сервіс: коли хтось попросить INoteService, DI видасть NoteService
        services.AddScoped<INoteService, NoteService>();

        return services;
    }
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Автоматично реєструє всі валідатори, що наслідуються від AbstractValidator в цій збірці
        services.AddValidatorsFromAssembly(typeof(NoteCreateRequestDto).Assembly);

        return services;
    }
}