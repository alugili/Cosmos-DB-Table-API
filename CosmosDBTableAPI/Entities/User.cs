using System;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosDBTableAPI
{
  public class User : TableEntity
  {
    public string EMail { get; set; }

    public DateTimeOffset LastLogin { get; set; }

    public User()
    {
    }

    public User(string locationId, string type)
    {
      PartitionKey = locationId;
      RowKey = type;
    }
  }
}