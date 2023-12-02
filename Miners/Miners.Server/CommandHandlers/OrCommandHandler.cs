using Miners.Server.CommandHandlers.Base;
using System;

namespace Miners.Server.CommandHandlers
{
    public class OrCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _first;
        private readonly ICommandHandler _second;

        public OrCommandHandler(ICommandHandler first, ICommandHandler second)
        {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public bool Handle(string request)
        {
            var result = _first.Handle(request);
            return result ? result : _second.Handle(request);
        }
    }
}
