using Miners.Data.LevelReaders.Base;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miners.Data.LevelReaders
{
    public class TXTReader : IReader
    {
        private readonly string _levelPath = ConfigurationManager.AppSettings["levelPath"].ToString();
        public void Read(string path)
        {
            throw new NotImplementedException();
        }
    }
}
