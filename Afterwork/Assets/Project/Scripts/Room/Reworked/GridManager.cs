using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class GridManager : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        
        private readonly IGridPlacementSystem _gridPlacementSystem = new GridPlacementSystem();
        private readonly IGridVisualizer _gridVisualizer = new GridVisualizer();
        
        private void Awake()
        {
            Vector2Int gridSie = new Vector2Int(10, 10);
            float tileSize = 1f;
            
            var grids  = CreateGrid(gridSie);
            
            this._gridPlacementSystem.Initialize(grids, tileSize);
            this._gridVisualizer.Initialize(grids, tileSize, this._meshFilter);
        }

        private Dictionary<Vector2Int, CellData> CreateGrid(Vector2Int gridSize)
        {
            Dictionary<Vector2Int, CellData> grid = new();
            
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int z = 0; z < gridSize.y; z++)
                {
                    grid.Add(new Vector2Int(x, z), new CellData());
                }
            }
            
            return grid;
        }

        public void IsEditMode(bool isEditMode)
        {
            this._gridVisualizer.SetVisibility(isEditMode);
        }
    }
}