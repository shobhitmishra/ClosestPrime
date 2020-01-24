using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi.Models.StorageProvider
{
    public class TableStorageProvider : IStorageProvider
    {
        const string tableName = "NumberEntityTable";
        private const string storageAccountErrorMessage =
            "Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.";
        private CloudTable _numberTable;
        private CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine(storageAccountErrorMessage);
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine(storageAccountErrorMessage);
                throw;
            }

            return storageAccount;
        }

        public CloudTable NumberTable
        {
            get
            {
                if (_numberTable == null)
                {
                    string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

                    // Retrieve storage account information from connection string.
                    CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

                    // Create a table client for interacting with the table service
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

                    // Create a table client for interacting with the table service 
                    CloudTable table = tableClient.GetTableReference(tableName);
                    table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
                    _numberTable = table;
                }
                return _numberTable;
            }
        }

        public async Task<NumberEntity> AddNumberEntityToTable(int number, int prime)
        {
            try
            {
                // First get the entity
                var numberEntity = await GetNumberEntity(number, prime).ConfigureAwait(false);
                if (numberEntity == null)
                {
                    numberEntity = new NumberEntity(number, prime)
                    {
                        Count = 0                        
                    };                    
                }               
                
                numberEntity.Count += 1;                

                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(numberEntity);

                // Execute the operation.
                TableResult result = await NumberTable.ExecuteAsync(insertOrMergeOperation).ConfigureAwait(false);
                var insertedNumber = result.Result as NumberEntity;
                
                return insertedNumber;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task<NumberEntity> GetNumberEntity(int number, int prime)
        {
            try
            {
                var numStr = number.ToString(CultureInfo.InvariantCulture);
                var primeStr = prime.ToString(CultureInfo.InvariantCulture);
                TableOperation retrieveOperation = TableOperation.Retrieve<NumberEntity>(numStr, primeStr);
                TableResult result = await NumberTable.ExecuteAsync(retrieveOperation).ConfigureAwait(false);
                var numberEntity = result.Result as NumberEntity;
                return numberEntity;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }

}

