using AnomalyWeb.Models.DB;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnomalyWeb.Service
{
    public class TableRepository
    {
        private CloudTable table;
        private CloudTableClient client;
        private CloudStorageAccount storageAccount;

        public TableRepository()
        {
            string tableStorageConnectionString = "KEY DB";
            storageAccount = CloudStorageAccount.Parse(tableStorageConnectionString);
            client = storageAccount.CreateCloudTableClient();
            table = client.GetTableReference("Transferencias");
        }

        public void AddorReplaceUser(TransferenciaEntity transferencia)
        {
            table.CreateIfNotExistsAsync();
            TableOperation insertOp = TableOperation.InsertOrReplace(transferencia);

            table.ExecuteAsync(insertOp);
        }

        public async Task<TransferenciaEntity> RetrieveTransferenciaAsync(string id, string name)
        {

            bool b = table.ExistsAsync().Result;

            TableOperation retOp = TableOperation.Retrieve<TransferenciaEntity>(id, name);

            TableResult tr = await table.ExecuteAsync(retOp);

            TransferenciaEntity transferencia = ((TransferenciaEntity)tr.Result);

            return transferencia;
        }


        public async Task<List<TransferenciaEntity>> RetrieveAllTransferenciasAsync()
        {

            List<TransferenciaEntity> transferencias = new List<TransferenciaEntity>();
            var query = new TableQuery<TransferenciaEntity>();

            TableContinuationToken continuationToken = null;
            do
            {
                var page = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = page.ContinuationToken;
                transferencias.AddRange(page.Results);
            }
            while (continuationToken != null);

            return transferencias;
        }
    }
}
