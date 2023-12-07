namespace AzCosmos;

public sealed class AddTodoCommand : Command
{
    private readonly TodosCollection _collection;
    private readonly PrintHelpCommand _printHelpCommand;

    public AddTodoCommand(TodosCollection collection, PrintHelpCommand printHelpCommand)
    {
        _collection = collection;
        _printHelpCommand = printHelpCommand;
    }

    public override void Handle(CancellationToken cancellationToken)
    {
        Todo todo = new();
        Console.Write($"Details of the Todo item:{Environment.NewLine}{Environment.NewLine}");
        todo.Description = ReadStringFromConsole("Description");
        todo.Category = ReadStringFromConsole("Category");

        _collection.AddAsync(todo, cancellationToken).GetAwaiter().GetResult();
        Console.WriteLine("Todo saved!");
        _printHelpCommand.Handle(cancellationToken);
    }

    private static string ReadStringFromConsole(string fieldName)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            string? fieldValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(fieldValue))
            {
                return fieldValue;
            }

            PrintFieldEmpty(fieldName);
        }
    }

    private static void PrintFieldEmpty(string fieldName)
    {
        Console.Error.WriteLine($"The {fieldName} should not be empty.");
    }
}