using System;
using System.Collections.Generic;

namespace MileFields;

public struct IndexIn2D
{
    public int VIndex { get; set; }
    public int HIndex { get; set; }
}

public class MineFieldsSolver
{
    public char[][] Solve(char[][] map)
    {
        throw new NotImplementedException();
    }

    public IndexIn2D[] AdjacencyList(int vIndex, int hIndex, int vCount, int hCount)
    {
        if (vIndex < 0 || hIndex < 0 || vCount < 0 || hCount < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        var indicesList = new List<IndexIn2D>();
        IndexIn2D? newIndexIn2D = null;

        for (int i = 0; i < vCount; ++i)
        {
            if (i == vIndex || i + 1 == vIndex || i - 1 == vIndex)
            {
                for (int j = 0; j < hCount; ++j)
                {
                }
            }
        }

        return indicesList.ToArray();
    }
}