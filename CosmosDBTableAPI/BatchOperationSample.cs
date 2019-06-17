using System;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBTableAPI
{
  public class BatchOperationSample
  {
    public void BatchOperation(CloudTable cloudTable)
    {
      Console.WriteLine("******************** Start Batch Operation ********************");
      var batchOperation = new TableBatchOperation();
      for (var i = 2; i < 52; i++)
      {
        // I will create and add users and send them as one batch to the table.
        var batchUser = new User("Karlsbad", "Admin_" + i)
        {
          EMail = "alugili@gmail.com",
          LastLogin = DateTimeOffset.UtcNow
        };

        batchOperation.Add(TableOperation.InsertOrMerge(batchUser));
      }

      // Executing the operations or adding the users.
      cloudTable.ExecuteBatch(batchOperation);
      Console.WriteLine("******************** End Batch Operation ********************");

    }
  }
}