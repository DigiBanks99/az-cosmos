namespace AzCosmos;

public sealed class PrintWelcomeMessageCommand : Command
{
    private readonly IEnumerable<Command> _subCommands;

    public PrintWelcomeMessageCommand(PrintHelpCommand helpCommand)
    {
        _subCommands = new[] { helpCommand };
    }

    public override void Handle(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Cosmos playground. You can view your Todos and add new ones.");

        foreach (Command command in _subCommands)
        {
            command.Handle(cancellationToken);
        }
    }
}