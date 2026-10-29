using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public interface IGridCreation
    {
        Dictionary<Vector2Int, CellData> CreateGrid();
    }

    public sealed class EmptyGridCreation : IGridCreation
    {
        public Dictionary<Vector2Int, CellData> CreateGrid()
        {
            return new Dictionary<Vector2Int, CellData>();
        }
    }

    public sealed class GridCreation : IGridCreation
    {
        private Vector2Int _gridSize;

        public GridCreation(Vector2Int gridSize)
        {
            this._gridSize = gridSize;
        }

        public Dictionary<Vector2Int, CellData> CreateGrid()
        {
            Dictionary<Vector2Int, CellData> grid = new();
            
            for (int x = 0; x < this._gridSize.x; x++)
            {
                for (int z = 0; z < this._gridSize.y; z++)
                {
                    grid.Add(new Vector2Int(x, z), new CellData());
                }
            }

            return grid;
        }
    }
}