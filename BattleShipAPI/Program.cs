using BattleShipAPI.Configuration;
using BattleShipAPI.Entities;
using BattleShipAPI.Middleware;
using BattleShipAPI.Services;
using BattleShipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BattleShipDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnect"));
});
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IShotService, ShotService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSingleton<GameConfig>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("FrontEndClient");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();

