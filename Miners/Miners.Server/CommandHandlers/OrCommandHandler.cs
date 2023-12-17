using Miners.Server.CommandHandlers.Base;
using System;

namespace Miners.Server.CommandHandlers
{
    public class OrCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _first;
        private readonly ICommandHandler _second;


        /// <summary>Initializes a new instance of the <see cref="OrCommandHandler" /> class.</summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <exception cref="System.ArgumentNullException">first
        /// or
        /// second</exception>
        public OrCommandHandler(ICommandHandler first, ICommandHandler second)
        {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        /// <inheritdoc />
        public bool Handle(string request)
        {
            var result = _first.Handle(request);
            return result ? result : _second.Handle(request);
        }
    }
}
