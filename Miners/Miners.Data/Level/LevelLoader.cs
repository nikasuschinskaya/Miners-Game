using Miners.Server.Level.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miners.Server.Level
{
    public class LevelLoader
    {
        private TXTReader _TXTReader;

        public int Width { get; set; }

        
        public int Height { get; set; }


        //BlockFactory factory = GetFactory(block);
        //IBlock block = factory.GetBlock();
        //private static BlockFactory GetFactory(BlockType blockType)
        //{
        //    switch (blockType)
        //    {
        //        case BlockType.None:
        //            break;
        //        case BlockType.SteadyBlock:
        //            return new SteadyBlockFactory()
        //        case BlockType.MediumStableBlock:
        //            break;
        //        case BlockType.WeakResistantBlock:
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}
