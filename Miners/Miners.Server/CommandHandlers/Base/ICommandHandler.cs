namespace Miners.Server.CommandHandlers.Base
{
    public interface ICommandHandler
    {
        bool Handle(string request);
    }
}
