using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace WebApplication1
{
    public interface ICosmosWeatherService
    { 
        IEnumerable<WeatherForecast> GetAll();
    }

    public class CosmosWeatherService : ICosmosWeatherService
    {
        private readonly Configuration.Configuration _config;

        public CosmosWeatherService(Configuration.Configuration config)
        {
            _config = config;
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            DocumentClient client = new DocumentClient(new Uri(_config.CosmosEndpointUri), _config.CosmosPrimaryKey, new ConnectionPolicy { EnableEndpointDiscovery = false });
            List<WeatherForecast> list = client.CreateDocumentQuery<WeatherForecast>(UriFactory.CreateDocumentCollectionUri(_config.CosmosDatabaseId, _config.CosmosContainerId).ToString(), new SqlQuerySpec("SELECT * FROM c"), new FeedOptions { MaxItemCount = -1 }).ToList<WeatherForecast>();
            return list;
        }
    }
}
