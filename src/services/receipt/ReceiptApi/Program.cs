using Microsoft.EntityFrameworkCore;
using ReceiptApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReceiptDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ReceiptDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    SeedData.InitializeDatabase(services.GetRequiredService<ReceiptDbContext>());
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
