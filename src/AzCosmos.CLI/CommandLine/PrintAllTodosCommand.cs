namespace AzCosmos;

public sealed class PrintAllTodosCommand : Command
{
    private readonly TodosCollection _collection;
    private readonly PrintHelpCommand _printHelpCommand;

    public PrintAllTodosCommand(TodosCollection collection, PrintHelpCommand printHelpCommand)
    {
        _collection = collection;
        _printHelpCommand = printHelpCommand;
    }

    public override void Handle(CancellationToken cancellationToken)
    {
        IEnumerable<Todo> todos = _collection.GetAllAsync(cancellationToken).ToBlockingEnumerable();
        foreach (Todo todo in todos)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Console.WriteLine($"- {todo}");
        }

        _printHelpCommand.Handle(cancellationToken);
    }
}