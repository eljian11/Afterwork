using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public interface IGridPlacementSystem 
    {
        void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize);
        Vector2Int WorldToGridPosition(Vector3 worldPosition);
        bool IsAreaAvailable(Vector2Int originCell, Vector2Int size);
    }
}