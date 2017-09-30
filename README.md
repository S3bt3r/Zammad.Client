# Zammad Client Library for .NET
The Zammad Client Library for .NET allows you to connect your .NET application to your Zammad instance.

## Features
- Group
    - List/Get/Create/Update/Delete
- OnlineNotification
    - List/Get/Create/Update/Delete/MarkAllAsRead
- Organization
    - List/Get/Create/Update/Delete/Search
- Ticket
    - List/Get/Create/Update/Delete/Search
- Ticket Article
    - List/Get/Create/ListForTicket
- Ticket Priority
    - List/Get/Create/Update/Delete
- Ticket State
    - List/Get/Create/Update/Delete
- User
    - List/Get/Create/Update/Delete/Search/GetMe

## Download & Install
The Client Library ships on NuGet. You'll find the latest version and hotfixes on NuGet via the `Zammad.Client` package.

### Via Git
To get the source code of the Library via git just type:

```bash
git clone https://github.com/Asesjix/Zammad-Client.git
cd Zammad-Client
```

### Via NuGet
To get the binaries of this library ready for use within your project you can also have them installed by [NuGet](https://www.nuget.org/packages/Zammad.Client).

Package Manager
```bash
Install-Package Zammad.Client
```

.NET CLI
```bash
dotnet add package Zammad.Client
```

## Dependencies

### Newtonsoft Json
The library depend on Newtonsoft Json, which can be downloaded directly or referenced by your code project through Nuget.

- [Newtonsoft.Json] (http://www.nuget.org/packages/Newtonsoft.Json)

## Code Samples

First, include the classes you need (in this case we'll include the Client and Ticket feature to demonstrate get all tickets):

```csharp
using Zammad.Client;
using Zammad.Client.Ticket;
```
To perform an operation you will first instantiate a *client* which allows performing actions on it.

```csharp
var account = ZammadAccount.CreateBasicAccount("https://contoso.zammad.com", "user", "password");
var ticketClient = account.CreateTicketClient();
```

Now, to get all tickets using the client:

```csharp
var ticketList = ticketClient.GetTicketListAsync();
```

Now, to create a ticket using the client:

```csharp
var ticket = await ticketClient.CreateTicketAsync(
	new Ticket
	{
		Title = "Help me!",
		GroupId = 1,
		CustomerId = 1,
	},
	new TicketArticle
	{
		Subject = "Help me!!!",
		Body = "Nothing Work!",
		Type = "note",
	});
```
