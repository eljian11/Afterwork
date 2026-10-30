using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public interface IGridPlacementSystem 
    {
        void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize);
        Vector2Int WorldToGridPosition(Vector3 worldPosition);
        Vector3 CellToWorldPosition(Vector2Int cellPos);
        bool IsAreaAvailable(Vector2Int originCell, Vector2Int size);
        void OccupyCell(Vector2Int cell, GameObject objectToOccupy);
        void UpdateGrid();
    }

    public sealed class EmptyGridPlacementSystem : IGridPlacementSystem
    {
        public void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize) { }

        public Vector2Int WorldToGridPosition(Vector3 worldPosition)
        {
            return new Vector2Int();
        }

        public Vector3 CellToWorldPosition(Vector2Int cellPos)
        {
            return new Vector3();
        }

        public bool IsAreaAvailable(Vector2Int originCell, Vector2Int size)
        {
            return false;
        }

        public void OccupyCell(Vector2Int cell, GameObject objectToOccupy) { }

        public void UpdateGrid() { }
    }
}