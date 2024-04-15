namespace WebApplication1.Configuration
{
    public class Configuration(string cosmosDatabaseId,string cosmosContainerId,string cosmosEndpointUri,string cosmosPrimaryKey)
    {
        public string CosmosDatabaseId{get;private set;} = cosmosDatabaseId;
        public string? CosmosContainerId{get;private set;} = cosmosContainerId;
        public string? CosmosEndpointUri{get;private set;} = cosmosEndpointUri;
        public string? CosmosPrimaryKey{get;private set;} = cosmosPrimaryKey;
    }
}
