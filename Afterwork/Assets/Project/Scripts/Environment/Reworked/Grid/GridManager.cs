using System.Collections.Generic;
using UnityEngine;

//gridmanger job: 
//creates the tiles -> grids
//checks if tiles are available 

namespace Project.Scripts.Room.Reworked
{
    public sealed class GridManager : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        
        private IGridPlacementSystem _gridPlacementSystem = new EmptyGridPlacementSystem(); // needs provider
        private IGridVisualizer _gridVisualizer = new EmptyGridVisualizer();  // maybe needs provider too?
        private IGridCreation _gridCreation = new EmptyGridCreation();
        public IGridPlacementSystem GridPlacementSystem => this._gridPlacementSystem;
        public IGridVisualizer GridVisualizer => this._gridVisualizer; 
        
        private void Awake() // still needs to be cleaned, needs in bootscene
        {
            Vector2Int gridSie = new Vector2Int(10, 10);
            float tileSize = 1f; // all tiles are 1x1

            this._gridPlacementSystem = new GridPlacementSystem();
            this._gridCreation = new GridCreation(gridSie);
            this._gridVisualizer = new GridVisualizer();
            
            var grids  = CreateGrid(gridSie);
            
            this._gridPlacementSystem.Initialize(grids, tileSize);
            this._gridVisualizer.Initialize(grids, tileSize, this._meshFilter);
        }
        
        private Dictionary<Vector2Int, CellData> CreateGrid(Vector2Int gridSize)
        {
            return this._gridCreation.CreateGrid();
        }

        public void IsEditMode(bool isEditMode)
        {
            this._gridVisualizer.SetVisibility(isEditMode);
        }
    }
}