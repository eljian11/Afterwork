using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    [Serializable]
    public class ItemPlacementGhost 
    {
        private Dictionary<Vector2Int, CellData> _grid;

        [SerializeField] private Material[] _ghostMaterials;
        [SerializeField] private Color _validColor = new Color(0.7f, 1f, 0.7f, 0.4f);   // Soft light green
        [SerializeField] private Color _invalidColor = new Color(1f, 0.6f, 0.6f, 0.4f); // Soft light red
        
        public void Initialize(Dictionary<Vector2Int, CellData> grid)
        {
            _grid = grid;

            foreach (CellData cellData in _grid.Values)
            {
                UpdateGhostState(cellData.IsOccupied);
            }
        }

        public void UpdateGhostState(bool isLegalPlacement)
        {
            Color targetColor = isLegalPlacement ? _validColor : _invalidColor;

            if (_ghostMaterials == null) return;

            foreach (var mat in _ghostMaterials)
            {
                if (mat == null) continue;
                
                if (mat.HasProperty("_Color"))
                {
                    mat.color = targetColor;
                }
                else if (mat.HasProperty("_BaseColor")) // For URP shaders
                {
                    mat.SetColor("_BaseColor", targetColor);
                }
            }
        }
    }
}