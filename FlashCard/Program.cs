using FlashCard.Configuration;
using FlashCard.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//added by mp

var connString = builder.Configuration.GetConnectionString("FlashCardDbConnections");
builder.Services.AddDbContext<FlashCardDbContext>(options => options.UseSqlServer(connString));

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowForAll", b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

// end

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//added by mp

app.UseCors("AllowForAll");
//end

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
