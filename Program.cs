using Microsoft.EntityFrameworkCore;
using MundoDeDisney.Core.Interfaces;
using MundoDeDisney.Infraestructure.Data;
using MundoDeDisney.Infraestructure.Mapping;
using MundoDeDisney.Infraestructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DisneyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));
builder.Services.AddScoped<IPersonajesRepositorio, PesonajeRepositorio>();
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
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

app.Run();
