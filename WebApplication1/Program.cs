using Azure.Identity;
using WebApplication1;
using WebApplication1.Configuration;
using WebApplication1.Controllers;
using Azure.Data.AppConfiguration;

string? azureConfigServiceConnectionString = Environment.GetEnvironmentVariable("AZURE_CONF_SERVICE_CONN_STRING");

azureConfigServiceConnectionString = "Endpoint=https://andywebapp1-appconfig.azconfig.io;Id=FOKu;Secret=Ak4geJz9JubrR0wdllQzO8OFTH3hoyuYvnqMnMfyPZc=";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
    options.Connect(azureConfigServiceConnectionString)
    .ConfigureKeyVault(kv =>
    {
        kv.SetCredential(new DefaultAzureCredential());
    })
);

Configuration configuration = GetConfiguration(builder.Configuration);

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

static Configuration GetConfiguration(IConfigurationRoot config)
{ 
    var databaseId = config["Database"];
    var containerId = config["Container"];
    var endpointUri = config["EndpointUri"];
    var primaryKey = config["PrimaryKey"];
    return new Configuration(databaseId,containerId,endpointUri,primaryKey);
}
