using System;
using System.Configuration;

namespace CosmosDBTableAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //const string enviromentName = "cloud";
      const string enviromentName = "emulator";

      var connectionString = ConfigurationManager.AppSettings[$"{enviromentName}:StorageConnectionString"];
      var tableName = ConfigurationManager.AppSettings[$"{enviromentName}:TableName"];

      var azureTableContext = new CosmosTableApiDbContext();
      var sa = azureTableContext.CreateCloudStorageAccount(connectionString);
      var cloudTable = azureTableContext.GetTableClient(tableName, sa);

      Console.WriteLine("Starting Demos!");

      // Demo for the basic CRUD operations.
      var crudOperationsSample = new CrudOperationsSample();
      crudOperationsSample.CrudOperations(cloudTable);

      // Demo for the batch operations.
      var batchOperationSample = new BatchOperationSample();
      batchOperationSample.BatchOperation(cloudTable);

      // Demo for the default Pessimistic Concurrency.
      var pessimisticConcurrency = new ConcurrencyDemo();
      pessimisticConcurrency.ConcurrencyDemoDefaultPessimistic(cloudTable);

      Console.WriteLine("Done!");
    }
  }
}