using Azure.Data.AppConfiguration;

namespace WebApplication1.Configuration
{
    public class Configuration
    { 
        public Configuration(string cosmosDatabaseId,string cosmosContainerId,string cosmosEndpointUri,string cosmosPrimaryKey)
        {
            CosmosDatabaseId = cosmosDatabaseId;
            CosmosContainerId = cosmosContainerId;
            CosmosEndpointUri = cosmosEndpointUri;
            CosmosPrimaryKey = cosmosPrimaryKey;
        }

        public Configuration(ConfigurationClient config)
        { 
            CosmosDatabaseId = GetSetting(config, "Database");
            CosmosContainerId = GetSetting(config, "Container");
            CosmosEndpointUri = GetSetting(config, "EndpointUri");
            CosmosPrimaryKey = GetSetting(config, "PrimaryKey");
        }

        public string CosmosDatabaseId{get;private set;}
        public string? CosmosContainerId{get;private set;}
        public string? CosmosEndpointUri{get;private set;}
        public string? CosmosPrimaryKey{get;private set;}

        private string GetSetting(ConfigurationClient config, string key)
        { 
            var setting = config.GetConfigurationSetting(key);
            return (setting != null) ? setting.Value.Value : ""; 
        }
    }
}
