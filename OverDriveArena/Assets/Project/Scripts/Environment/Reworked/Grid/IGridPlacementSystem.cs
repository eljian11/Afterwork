using System.Collections.Generic;
using Project.Scripts.Room.Reworked;
using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Grid
{
    public interface IGridPlacementSystem
    {
        void Initialize(ref Dictionary<Vector2Int, CellData> grid, float tileSize);
        Vector2Int WorldToGridPosition(Vector3 worldPosition);
        Vector3 CellToWorldPosition(Vector2Int cellPos);
        bool IsAreaAvailable(Vector2Int originCell, Vector2Int size);
        void OccupyCell(Vector2Int cell, GameObject objectToOccupy);
        void UpdateGrid();
    }
}