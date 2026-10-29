using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public interface IGridVisualizer
    {
        void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize, MeshFilter meshFilter);
        void SetVisibility(bool isVisible);
        void UpdateGrid();
    }
}