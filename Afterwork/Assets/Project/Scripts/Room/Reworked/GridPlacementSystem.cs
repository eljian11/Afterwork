using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class GridPlacementSystem : IGridPlacementSystem 
    {
        private float _tileSize = 1f;
        private Dictionary<Vector2Int, CellData> grid = new();

        public void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize)
        {
            this.grid = grid;
            this._tileSize = tileSize;
        }

        public Vector2Int WorldToGridPosition(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x / this._tileSize);
            int z = Mathf.FloorToInt(worldPosition.z / this._tileSize);
            
            return new Vector2Int(x, z);
        }
        
        public bool IsAreaAvailable(Vector2Int originCell, Vector2Int size)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int z = 0; z < size.y; z++)
                {
                    Vector2Int targetCell = new Vector2Int(originCell.x + x, originCell.y + z);
                
                    if (IsCellFree(targetCell) == false) 
                    {
                        return false; 
                    }
                }
            }
            return true;
        }
    
        private bool IsCellFree(Vector2Int cellPos)
        {
            if (this.grid.TryGetValue(cellPos, out var cell))
            {
                return !cell.IsOccupied;
            }
            return true;
        }
    }
}