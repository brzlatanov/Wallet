public interface ICommandHandler
{
    bool CanHandle(string command);
    string Handle(Decimal amount);
}