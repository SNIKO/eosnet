# eosnet
An asynchronous, non-blocking, .net core implementation of the client for EOS blockchain (https://eos.io/). The library contains an RPC client as well as a wallet for managing keys and signing transactions.

## Getting started
The library is [available on NuGet](https://www.nuget.org/packages/eosnet/2.0.0)

Install using Package Manager Console:

```
Install-Package eosnet
```

## Examples
Create a wallet with private keys which will be responsible for signing transactions:
``` csharp
var wallet = new EosWallet(new []
{
  "5KQwrPbwdL6PhXujxW37FSSQZ1JiwsST4cqQzDeyXtP79zkvFD3",     // private key for myaccount111
  "5KKXE5rsqQ1niB4hKTJkvBGJHgguFfta41vHLaWGR1jKXrG2BbL"      // private key for myaccount222
});
```

Create a client for interaction with the blockchain. By default, client connects to a local EOS instance (nodeos) running on default host: http://localhost:8888
``` csharp
var client = new EosClient(wallet);
```

The node can be overridden in the constructor:
``` csharp
var client = new EosClient(new Uri("http://api.eosnewyork.io"), wallet);
```

The simplest way to trigger a smart contract is to push an action. Remember, there must be a private key in the wallet for an account(s) specified in the authorization section: 
``` csharp
var transactionId = await client.PushActionsAsync(new []
{
    new EOS.Client.Models.Action()
    {
        Account = "eosio.token",
        Name = "transfer",
        Authorization = new []
        {
            new Authorization
            {
                Actor = "myaccount111",
                Permission = "active"
            }
        },
        Data = new Dictionary<string, object>
        {
            {"from", "myaccount111"},
            {"to", "someuserrrrr"},
            {"quantity", "100.0000 EOS"},
            {"memo", ""}
        }                            
    }
});
```

The code above will generate a transaction with default parameters and the specified action. To generate a custom transaction use PushTransactionAsync method: 
``` csharp
var transactionId = await client.PushTransactionAsync(new Transaction
{                    
    RefBlockNum = 11762412,                               // mandatory
    RefBlockPrefix = 15881242,                            // mandatory
    Expiration = DateTime.Parse("2018-08-20T13:13:13Z"),  // mandatory
    /* other optional parametrs */
    Actions = new []
    {
        new EOS.Client.Models.Action()
        {
            Account = "eosio.token",
            Name = "transfer",
            Authorization = new []
            {
                new Authorization
                {
                    Actor = "myaccount111",
                    Permission = "active"
                }
            },
            Data = new Dictionary<string, object>
            {
                {"from", "myaccount111"},
                {"to", "someuserrrrr"},
                {"quantity", "100.0000 EOS"},
                {"memo", ""}
            }                            
        }
    }
});
```

Interaction with the blockchain:
``` csharp
// Get status of the blockchain
var chainInfo = await client.Api.GetInfoAsync();

// Get info about the account eosnewyorkio
var account = await client.Api.GetAccountAsync("eosnewyorkio");

// Get a concrete block using the block number or unique id
var block = await client.Api.GetBlockAsync("5485906");

// Get a balance of the everipedia token (IQ) for account eosnewyorkio
var balance = await client.Api.GetCurrencyBalanceAsync("everipediaiq", "eosnewyorkio", "IQ");
                
// Sending a transaction into the blockchain. The transaction must be signed using EosWallet 
// or any other signature provider:         
var res = await client.Api.PushTransactionAsync(signedTransaction);
```

All methods throw either HttpRequestException (if there were a network error) or HttpResponseException (if the node returned an unsuccessful result).
