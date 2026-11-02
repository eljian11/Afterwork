using System.Collections.Generic;
using Project.Scripts.Environment.Reworked.Grid;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class EmptyGridVisualizer : IGridVisualizer
    {
        public void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize, MeshFilter meshFilter)
        {
        }

        public void SetVisibility(bool isVisible)
        {
        }

        public void UpdateGrid()
        {
        }
    }
}