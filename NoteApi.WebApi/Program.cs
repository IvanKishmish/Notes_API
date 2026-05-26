using DotNetEnv;
using NoteApi.Persistence; // Підключаємо наш шар Persistence

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 1. Сюди додаємо сервіси
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ОДИН РЯДОК, який підключає всю нашу базу і сервіси завдяки DependencyInjection.cs
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

// 2. Налаштовуємо HTTP-пайплайн (Middlewares)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Дозволяє використовувати класи-контролери

app.Run();