using System.Collections.Generic;
using Project.Scripts.Room.Reworked;
using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Grid
{
    public interface IGridVisualizer
    {
        void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize, MeshFilter meshFilter);
        void SetVisibility(bool isVisible);
        void UpdateGrid();
    }
}