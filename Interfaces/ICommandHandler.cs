public interface ICommandHandler
{
    bool CanHandle(string command);
    Task<string> Handle(Decimal amount);
}