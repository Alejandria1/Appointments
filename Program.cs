using Appointments;
using Appointments.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAvailableTimesDay, AvailableTimesDay>();
/*
 * database context dependency Injection
 */
var dbHost = "localhost";
var dbName = "APPOINTMENTS";
var dbPort = 3306;
var dbPassword = "Bernaes4dmin100.";

var connectionString = $"server={dbHost};port={dbPort};database={dbName};user=root;password={dbPassword}";

builder.Services.AddDbContext<AppointmentsDBContext>(options => options.UseMySQL(connectionString));
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

app.Run();
