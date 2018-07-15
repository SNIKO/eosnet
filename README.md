# eosnet
An asynchronous, non-blocking, .net core implementation of the client for EOS blockchain (https://eos.io/)

## Getting started
The library is [available on NuGet](https://www.nuget.org/packages/eosnet/1.0.0)

Install using Package Manager Console:

```
Install-Package eosnet
```

## Examples

By default, the client connects to the local EOS instance (nodeos) run on a default host: http://localhost:8888
``` csharp
var client = new EosClient();
```

The node can be overridden in the constructor:
``` csharp
var client = new EosClient("http://api.eosnewyork.io");
```

Interaction with the blockchain:
```
// Get status of the blockchain
var chainInfo = await client.GetInfoAsync();

// Get info about the account eosnewyorkio
var account = await client.GetAccountAsync("eosnewyorkio");

// Get a concrete block using the block number or unique id
var block = await client.GetBlockAsync("5485906");

// Get a balance of the everipedia token (IQ) for account eosnewyorkio
var balance = await client.GetCurrencyBalanceAsync("everipediaiq", "eosnewyorkio", "IQ");
                
// Sending a transaction into the blockchain. The transaction must be preliminarily signed.                
var res = await client.PushTransactionAsync(signedTransaction);
```

All methods throw either HttpRequestException (if there were a network error) or HttpResponseException (if the node returned an unsuccessful result).