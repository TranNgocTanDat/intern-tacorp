using beSQLSugar.Data;
using beSQLSugar.Repository;
using beSQLSugar.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Auto Mapper Configurations
builder.Services.AddAutoMapper(typeof(Program));

// Add SQLSugar
builder.Services.AddScoped<DbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));


builder.Services.AddScoped<IDeviceTypeService, DeviceTypeService>();


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
