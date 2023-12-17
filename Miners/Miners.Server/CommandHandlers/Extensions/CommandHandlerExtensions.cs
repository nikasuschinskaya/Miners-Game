using Miners.Server.CommandHandlers.Base;

namespace Miners.Server.CommandHandlers.Extensions
{
    public static class CommandHandlerExtensions
    {
        /// <summary>Ors the specified other.</summary>
        /// <param name="first">The first.</param>
        /// <param name="other">The other.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static ICommandHandler Or(this ICommandHandler first, ICommandHandler other) =>
            new OrCommandHandler(first, other);
    }
}
