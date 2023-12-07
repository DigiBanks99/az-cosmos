using Microsoft.Extensions.DependencyInjection;

namespace AzCosmos;

public sealed class CommandLine
{
    private readonly IServiceProvider _serviceProvider;

    public CommandLine(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Handle(CancellationToken cancellationToken)
    {
        var welcomeCommand = _serviceProvider.GetRequiredService<PrintWelcomeMessageCommand>();
        welcomeCommand.Handle(cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            Console.TreatControlCAsInput = true;
            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.WriteLine();

            Console.TreatControlCAsInput = false;
            Command command;
            if ((key.Modifiers & ConsoleModifiers.Control) != 0)
            {
                command = key.Key switch
                {
                    ConsoleKey.C => _serviceProvider.GetRequiredService<QuitCommand>(),
                    _ => _serviceProvider.GetRequiredService<PrintHelpCommand>()
                };
            }
            else
            {
                command = key.Key switch
                {
                    ConsoleKey.A => _serviceProvider.GetRequiredService<AddTodoCommand>(),
                    ConsoleKey.V => _serviceProvider.GetRequiredService<PrintAllTodosCommand>(),
                    _ => _serviceProvider.GetRequiredService<PrintHelpCommand>()
                };
            }

            try
            {
                command.Handle(cancellationToken);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}