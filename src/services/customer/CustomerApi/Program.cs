using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CustomerDatabase")));

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
    SeedData.InitializeDatabase(services.GetRequiredService<CustomerDbContext>());
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
