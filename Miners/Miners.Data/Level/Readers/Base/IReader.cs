using System.Collections.Generic;

namespace Miners.Server.Level.Readers.Base
{
    public interface IReader
    {
        List<string> Read();
    }
}
