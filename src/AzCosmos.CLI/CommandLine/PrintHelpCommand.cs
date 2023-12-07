namespace AzCosmos;

public sealed class PrintHelpCommand : Command
{
    public override void Handle(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        Console.Write("Options: A - Add Todo, V - View All Todos, Ctrl+C - Quit: ");
    }
}