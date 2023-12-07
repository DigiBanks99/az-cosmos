namespace AzCosmos;

public abstract class Command
{
    public abstract void Handle(CancellationToken cancellationToken);
}