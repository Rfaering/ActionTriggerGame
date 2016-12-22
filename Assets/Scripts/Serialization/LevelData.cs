using System;

[Serializable()]
public class LevelData
{
    public int Rows;
    public int Columns;

    public bool Mirror;

    public TileData[] Tiles;
}
