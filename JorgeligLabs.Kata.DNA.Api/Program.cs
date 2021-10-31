using JorgeligLabs.Kata.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IEvaluationService, EvaluationService>();
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              { securitySchema, new[] { "Bearer" } }
          });
});

// 1. Add Authentication Services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration.GetSection("AuthenticationOptions:Authority").Value; 
    options.Audience = builder.Configuration.GetSection("AuthenticationOptions:Audience").Value;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// 2. Enable authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", ((Func<string>) (() => "Welcome to Mutant Api")));

app.Run();
