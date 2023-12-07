using AzCosmos;
using AzCosmos.CLI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHostBuilder builder = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) => services
        .AddTodoCollection(context.Configuration, context.HostingEnvironment)
        .AddCommandLine()
    )
    .UseConsoleLifetime();
IHost app = builder.Build();

await app.StartAsync();

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

var cosmosCollection = app.Services.GetRequiredService<TodosCollection>();

try
{
    cosmosCollection.Initialize();
}
catch (Exception e)
    when (e is TaskCanceledException or OperationCanceledException)
{
    Console.Error.WriteLine("Failed to initialize the Cosmos collection in time");
    await app.StopAsync(lifetime.ApplicationStopping);
    return;
}

var commandLine = app.Services.GetRequiredService<CommandLine>();

commandLine.Handle(lifetime.ApplicationStopping);