using System;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    [Serializable]
    public class ItemPlacementGhost 
    {
        private Renderer[] _ghostRenderers;

        [SerializeField] private Color _validColor = new Color(0.7f, 1f, 0.7f, 0.4f);   
        [SerializeField] private Color _invalidColor = new Color(1f, 0.6f, 0.6f, 0.4f); 
        
        public void Initialize(GameObject ghostPrefabInstance)
        {
            if (ghostPrefabInstance != null)
            {
                this._ghostRenderers = ghostPrefabInstance.GetComponentsInChildren<Renderer>();
            }
        }

        public void UpdateGhostState(bool isLegalPlacement)
        {
            Color targetColor = isLegalPlacement ? this._validColor : this._invalidColor;

            if (this._ghostRenderers == null) return;

            foreach (var rend in this._ghostRenderers)
            {
                if (rend == null) continue;

                foreach (var mat in rend.materials)
                {
                    if (mat == null) continue;
                    
                    if (mat.HasProperty("_Color"))
                    {
                        mat.color = targetColor;
                    }
                    else if (mat.HasProperty("_BaseColor")) 
                    {
                        mat.SetColor("_BaseColor", targetColor);
                    }
                }
            }
        }
    }
}