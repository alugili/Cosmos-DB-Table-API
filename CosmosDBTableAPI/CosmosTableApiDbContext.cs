using Microsoft.Azure.Cosmos.Table;
using System;

namespace CosmosDBTableAPI
{
  public class CosmosTableApiDbContext
  {
    public CloudStorageAccount CreateCloudStorageAccount(string connectionString)
    {
      return CloudStorageAccount.Parse(connectionString);
    }

    public CloudTable GetTableClient(string tableName, CloudStorageAccount storageAccount)
    {

      var tableClient = storageAccount.CreateCloudTableClient();

      var table = tableClient.GetTableReference(tableName);

      // Create a table client for interacting with the table service 
      if (table.CreateIfNotExists())
      {
        Console.WriteLine("Created Table named: {0}", tableName);
      }
      else
      {
        Console.WriteLine("Table {0} already exists", tableName);
      }
      return table;
    }
  }
}