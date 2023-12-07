using Microsoft.Extensions.Hosting;

namespace AzCosmos;

public sealed class QuitCommand : Command
{
    private readonly IHostApplicationLifetime _lifetime;

    public QuitCommand(IHostApplicationLifetime lifetime)
    {
        _lifetime = lifetime;
    }

    public override void Handle(CancellationToken cancellationToken)
    {
        Console.WriteLine("Exiting...");
        _lifetime.StopApplication();
    }
}