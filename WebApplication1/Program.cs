using Azure.Identity;
using WebApplication1;
using WebApplication1.Configuration;
using WebApplication1.Controllers;
using Azure.Data.AppConfiguration;

string? azureConfigServiceConnectionString = Environment.GetEnvironmentVariable("AZURE_CONF_SERVICE_CONN_STRING");
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
    options.Connect(azureConfigServiceConnectionString)
    .ConfigureKeyVault(kv =>
    {
        kv.SetCredential(new DefaultAzureCredential());
    })
);

var configClient = new ConfigurationClient(azureConfigServiceConnectionString);
Configuration configuration = new(configClient);

// Add services to the container.
builder.Services.AddSingleton<ICosmosWeatherService>(new CosmosWeatherService(configuration));
builder.Services.AddSingleton<ILogger<WeatherForecastController>, Logger<WeatherForecastController>>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

