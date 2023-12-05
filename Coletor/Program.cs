using Microsoft.EntityFrameworkCore;
using HotSpotAPI.Models.Data;
using HotSpotAPI.Models.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//FIX para essa Injeção de Dependencia da Base de Dados. Override no Contexto.
var connectionString = builder.Configuration.GetConnectionString("HotSpot");
builder.Services.AddDbContext<HotSpotContext>(context => 
    context.UseSqlite("HotSpot"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// FIX para remover erro de blocked by CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200",
                            "http://localhost:4200/landing-page"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

// app.UseCors(x => x.AllowAnyHeader()
//                               .AllowAnyMethod()
//                               .AllowAnyOrigin());


app.MapControllers();

app.Run();
