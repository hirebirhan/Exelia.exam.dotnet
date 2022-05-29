using Excelia.exam.Application.CQRS.Queries;
using Exelia.exam.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration; // allows both to access and to set up the config

// Add services to the container.

services.AddControllers();

var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<BeerCollectionDbContext>(
   options =>
       options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddMediatR(typeof(CreateBeerQuery));
services.AddMediatR(typeof(GetBeersQuery));



services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Beer collection api", Version = "v1" });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    // use context to run initial migration
    var dbContext = scope.ServiceProvider.GetRequiredService<BeerCollectionDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
