using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi.Models.StorageProvider
{
    public class CosmosDbStorageProvider : IStorageProvider
    {
        const string databaseId = "ClosestPrimeToNumbers";
        const string numberPrimeContainerId = "NumberToPrimeContainer";
        const string maxDiffContainerId = "MaxDiffContainer";

        private CosmosClient _cosmosClient;
        private Database _database;
        private Container _numberToPrimeContainer;
        private Container _maxDiffContainer;        

        public CosmosClient CosmosClient
        {
            get
            {
                if (_cosmosClient == null)
                {
                    var endpointUri = AppSettings.LoadAppSettings().EndPointUri;
                    var primaryKey = AppSettings.LoadAppSettings().PrimaryKey;

                    // create the Cosmos Client
                    _cosmosClient = new CosmosClient(endpointUri, primaryKey);
                }
                return _cosmosClient;
            }
        }

        private Database NumberToPrimeDatabase
        {
            get
            {
                if (_database == null)
                {
                    var databaseResponse = CosmosClient
                        .CreateDatabaseIfNotExistsAsync(databaseId)
                        .GetAwaiter().GetResult();
                    _database = databaseResponse.Database;
                }
                return _database;
            }
        }

        private Container NumberToPrimeContainer
        {
            get
            {
                if (_numberToPrimeContainer == null)
                {
                    var containerResponse = NumberToPrimeDatabase
                        .CreateContainerIfNotExistsAsync(numberPrimeContainerId, "/MatchedPrime")
                        .GetAwaiter().GetResult();
                    _numberToPrimeContainer = containerResponse.Container;
                }
                return _numberToPrimeContainer;
            }
        }

        private Container MaxDiffContainer
        {
            get
            {
                if (_maxDiffContainer == null)
                {
                    var containerResponse = NumberToPrimeDatabase
                        .CreateContainerIfNotExistsAsync(maxDiffContainerId, $"/PartitionKey")
                        .GetAwaiter().GetResult();
                    _maxDiffContainer = containerResponse.Container;
                }
                return _maxDiffContainer;
            }
        }

        private async Task<NumberEntity> FindNumberEntity(int number, int prime)
        {
            var numEntity = new NumberEntity(number, prime);
            return await FindNumberEntity(numEntity).ConfigureAwait(false);
        }

        private async Task<NumberEntity> FindNumberEntity(NumberEntity numEntity)
        {
            try
            {
                var numEntityResponse = await NumberToPrimeContainer.ReadItemAsync<NumberEntity>(
                    numEntity.Id, new PartitionKey(numEntity.MatchedPrime)).ConfigureAwait(false);
                return numEntityResponse.Resource;
            }

            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Log the error                
                return null;
            }
        }


        private async Task UpdateMaxDifferenceEntity(int number, int prime)
        {
            var maxDiffEntity = await GetMaxDifferenceEntity().ConfigureAwait(false);            
            var newMaxDiffEntity = new MaxDifferenceEntity(number, prime);
            if (maxDiffEntity == null)
            {
                // Create maxDiffEntity
                await MaxDiffContainer.CreateItemAsync(
                    newMaxDiffEntity, new PartitionKey(newMaxDiffEntity.PartitionKey)).ConfigureAwait(false);
            }
            else if(newMaxDiffEntity.Difference > maxDiffEntity.Difference)
            {
                await MaxDiffContainer.ReplaceItemAsync(newMaxDiffEntity, 
                    MaxDifferenceEntity.IdAndPartitionString, 
                    new PartitionKey(newMaxDiffEntity.PartitionKey)).ConfigureAwait(false);
            }
            else if(newMaxDiffEntity.Difference == maxDiffEntity.Difference)
            {
                newMaxDiffEntity.Count = maxDiffEntity.Count + 1;
                await MaxDiffContainer.ReplaceItemAsync(newMaxDiffEntity,
                    MaxDifferenceEntity.IdAndPartitionString,
                    new PartitionKey(newMaxDiffEntity.PartitionKey)).ConfigureAwait(false);
            }
        }

        public async Task<MaxDifferenceEntity> GetMaxDifferenceEntity()
        {
            try
            {
                var maxDifferenceEntityResponse = await MaxDiffContainer.ReadItemAsync<MaxDifferenceEntity>(
                    MaxDifferenceEntity.IdAndPartitionString, new PartitionKey(MaxDifferenceEntity.IdAndPartitionString)).ConfigureAwait(false);
                return maxDifferenceEntityResponse.Resource;
            }

            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Log the error                
                return null;
            }
        }

        public async Task<NumberEntity> AddNumberEntityToTable(int number, int prime)
        {
            var numEntity = await FindNumberEntity(number, prime).ConfigureAwait(false);
            if (numEntity != null)
            {
                numEntity.UpdateItem();
                await NumberToPrimeContainer.ReplaceItemAsync(
                numEntity, numEntity.Id, new PartitionKey(numEntity.MatchedPrime))
                .ConfigureAwait(false);
            }
            else
            {
                numEntity = new NumberEntity(number, prime);
                await NumberToPrimeContainer.CreateItemAsync(numEntity, new PartitionKey(numEntity.MatchedPrime)).ConfigureAwait(false);
            }
            
            await UpdateMaxDifferenceEntity(number, prime).ConfigureAwait(false);
            return numEntity;
        }

        public async Task<NumberEntity> GetNumberEntity(int number, int prime)
        {
            return await FindNumberEntity(number, prime).ConfigureAwait(false);
        }

        public async Task<List<NumberEntity>> GetAllNumberEntities()
        {
            var iterator = NumberToPrimeContainer.GetItemQueryIterator<NumberEntity>();
            var result = new List<NumberEntity>();
            while (iterator.HasMoreResults)
            {
                var items = await iterator.ReadNextAsync().ConfigureAwait(false);
                result.AddRange(items);
            }
            return result;
        }
    }
}
