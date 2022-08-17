using KarmaStore;
using KarmaStore.DTO;
using KarmaStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("defaultConectionString");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseSqlServer(connection);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(setup =>
//{
//    setup.OperationFilter<FileUploadOperationFilter>();
//});
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
