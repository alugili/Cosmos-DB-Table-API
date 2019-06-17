using System;
using System.Net;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBTableAPI
{
  public class ConcurrencyDemo
  {
    public void ConcurrencyDemoDefaultPessimistic(CloudTable cloudTable)
    {
      Console.WriteLine("**************************** Start Demonstrate pessimistic concurrency ****************************");

      // Add new user to table.
      var firstUser = new User("Karlsruhe", "Operator")
      {
        EMail = "alugili@gmail.com",
        LastLogin = DateTimeOffset.UtcNow
      };

      var insertOrReplaceOperation = TableOperation.InsertOrReplace(firstUser);
      cloudTable.Execute(insertOrReplaceOperation);
      Console.WriteLine("Entity added. Original ETag = {0}", firstUser.ETag);

      // Someone else has changed the first user!
      var updatedFirstUser = new User("Karlsruhe", "Operator")
      {
        EMail = "bassam.alugili@hotmail.de",
        LastLogin = DateTimeOffset.UtcNow
      };

      insertOrReplaceOperation = TableOperation.InsertOrReplace(updatedFirstUser);
      cloudTable.Execute(insertOrReplaceOperation);
      Console.WriteLine("Entity updated. Updated ETag = {0}", updatedFirstUser.ETag);

      // Try updating first user. Etag is cached within firstUser and passed by default
      firstUser.LastLogin = DateTimeOffset.UtcNow;

      insertOrReplaceOperation = TableOperation.Merge(firstUser);
      try
      {
        Console.WriteLine("Trying to update Original entity");
        cloudTable.Execute(insertOrReplaceOperation);
      }
      catch (StorageException ex)
      {
        if (ex.RequestInformation.HttpStatusCode == (int)HttpStatusCode.PreconditionFailed)
        {
          Console.WriteLine("Error: Entity Tag is changed!");
        }
        else
        {
          throw;
        }
      }
      Console.WriteLine("**************************** End Demonstrate pessimistic concurrency ****************************");
    }
  }
}
