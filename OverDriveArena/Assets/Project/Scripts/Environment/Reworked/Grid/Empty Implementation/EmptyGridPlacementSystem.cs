using System.Collections.Generic;
using Project.Scripts.Environment.Reworked.Grid;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class EmptyGridPlacementSystem : IGridPlacementSystem
    {
        public void Initialize(ref Dictionary<Vector2Int, CellData> grid, float tileSize)
        {
        }

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

        public void OccupyCell(Vector2Int cell, GameObject objectToOccupy)
        {
        }

        public void UpdateGrid()
        {
        }
    }
}