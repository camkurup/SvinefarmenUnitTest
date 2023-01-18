using Microsoft.EntityFrameworkCore;
using SvinefarmenUnitTest;
using SvinefarmenUnitTest.Interface;
using SvinefarmenUnitTest.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILight, LightRepository>();
builder.Services.AddDbContext<ThePigFarmContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("¨ThePigFarm")));

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
