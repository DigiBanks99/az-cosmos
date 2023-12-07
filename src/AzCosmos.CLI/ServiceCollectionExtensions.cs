using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzCosmos.CLI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandLine(this IServiceCollection services)
    {
        return services
            .AddSingleton<AddTodoCommand>()
            .AddSingleton<PrintAllTodosCommand>()
            .AddSingleton<PrintHelpCommand>()
            .AddSingleton<PrintWelcomeMessageCommand>()
            .AddSingleton<QuitCommand>()
            .AddSingleton<CommandLine>();
    }
}