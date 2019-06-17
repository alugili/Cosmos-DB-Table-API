using System;
using System.Diagnostics;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBTableAPI
{
  public class CrudOperationsSample
  {
    public void CrudOperations(CloudTable cloudTable)
    {
      // Create a user.
      var user = new User("Karlsbad", "Admin")
      {
        EMail = "alugili@gmail.com",
        LastLogin = DateTimeOffset.UtcNow
      };

      Console.WriteLine("******************** Create a user ********************");

      // Create the insert replace operation.
      var insertOrReplaceOperation = TableOperation.InsertOrReplace(user);

      // Execute the operation. I have ignored the result just for demo.
      _ = cloudTable.Execute(insertOrReplaceOperation);

      Console.WriteLine($"user is created {user.ETag}");

      // In the Production Code !You can evaluate result to check that the operation has successfully finished!.

      Console.WriteLine("******************** Update a user ********************");
      // Create the insert merge operation.
      var insertOrMergeOperation = TableOperation.InsertOrMerge(user);

      user.EMail = "bassam.alugili@hotmail.de";

      _ = cloudTable.Execute(insertOrMergeOperation);

      Console.WriteLine($"user is updated {user.ETag}");

      Console.WriteLine("******************** Find a user ********************");
      // Create the retrieve operation.
      var retrieveOperation = TableOperation.Retrieve<User>("Karlsbad", "Admin");

      // Find the entity with PartitionKey ="Karlsbad" and RowKey="Admin"
      var retrieveResult = cloudTable.Execute(retrieveOperation).Result;

      var retrievedUser = retrieveResult as User;

      Console.WriteLine($"user is found {retrievedUser?.ETag}");

      Debug.Assert(retrievedUser?.EMail == "bassam.alugili@hotmail.de");

      Console.WriteLine("******************** Delete a user ********************");

      // Delete the retrieve user.
      var deleteOperation = TableOperation.Delete(retrievedUser);
      _ = cloudTable.Execute(deleteOperation);
      Console.WriteLine($"user is deleted {retrievedUser.ETag}");
    }
  }
}