using JorgeligLabs.Kata.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IEvaluationService, EvaluationService>();
builder.Services.AddSingleton<IStorageService, StorageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", ((Func<string>) (() => "Welcome to Mutant Api")));

app.Run();
