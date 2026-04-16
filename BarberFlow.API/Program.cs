using BarberFlow.Application.Interfaces;
using BarberFlow.Application.Services;
using BarberFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar serviços
builder.Services.AddScoped<IServiceService, ServiceService>();


// DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers (ESSENCIAL)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  Swagger só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Mapeia controllers
app.MapControllers();


app.Run();

