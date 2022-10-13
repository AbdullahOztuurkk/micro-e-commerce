using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string configFile = Path.Combine(Directory.GetCurrentDirectory().ToLower(),"Configurations/ocelot.json");

builder.Services
    .AddOcelot(new ConfigurationBuilder()
        .AddJsonFile(configFile)
        .AddEnvironmentVariables().Build());

builder.Services.AddControllers();
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

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
