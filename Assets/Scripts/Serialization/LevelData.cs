using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Serialization
{
    [Serializable()]
    public class LevelData
    {
        public int Rows;
        public int Columns;

        public bool Mirror;

        public TileData[] Tiles;
    }
}
