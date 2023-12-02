using Miners.Server.CommandHandlers.Base;

namespace Miners.Server.CommandHandlers.Extensions
{
    public static class CommandHandlerExtensions
    {
        public static ICommandHandler Or(this ICommandHandler first, ICommandHandler other) =>
            new OrCommandHandler(first, other);
    }
}
