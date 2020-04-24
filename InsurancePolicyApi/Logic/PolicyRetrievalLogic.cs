using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsurancePolicyApi.Controllers;
using InsurancePolicyApi.Interfaces;
using InsurancePolicyApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace InsurancePolicyApi.Logic
{
    public class PolicyRetrievalLogic : IRetrievePolicies
    {
        private readonly AppSettings _settings;
        private readonly ILogger<PolicyController> _logger;

        public PolicyRetrievalLogic(ILogger<PolicyController> logger, IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            _logger = logger;
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            var table = GetPoliciesTable();
            var results = await GetAllTableEntitiesAsync(table);
            return results;
        }

        private CloudTable GetPoliciesTable()
        {
            string accountName = _settings.AzureSettings.AzureAccountName;
            string accountKey = _settings.AzureSettings.AzureAccountKey;
            string tableName = _settings.AzureSettings.PoliciesTableName;
            try
            {
                StorageCredentials credentials = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(credentials, useHttps: true);
                CloudTableClient client = account.CreateCloudTableClient();
                CloudTable table = client.GetTableReference(tableName);
                return table;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Example log message - {ex}");
            }
            return null;
        }

        private async Task<IEnumerable<Policy>> GetAllTableEntitiesAsync(CloudTable table)
        {
            TableQuerySegment<Policy> entities = await table.ExecuteQuerySegmentedAsync(new TableQuery<Policy>(), null);
            return entities.Results.ToList();
        }
    }
}
