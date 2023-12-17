namespace Miners.Server.CommandHandlers.Base
{
    public interface ICommandHandler
    {

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///  Was the request handle?
        /// </returns>
        bool Handle(string request);
    }
}
