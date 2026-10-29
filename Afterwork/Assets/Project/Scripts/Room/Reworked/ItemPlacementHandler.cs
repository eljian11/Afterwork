using System;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class ItemPlacementHandler : MonoBehaviour
    {
        [SerializeField] private ItemPlacementGhost _ghost = new();
        
        private ItemData _currentItemData;
        private int _rotationStep = 0;
        private GameObject _activeItem;

        public void Initialize(ItemData data)
        {
            this._currentItemData = data;
            this._rotationStep = 0;
            this.transform.rotation = Quaternion.identity;

            CleanupGhost();

            if (this._currentItemData != null && this._currentItemData.prefab != null)
            {
                this._activeItem = Instantiate(this._currentItemData.prefab, this.transform);
                this._activeItem.transform.localPosition = Vector3.zero;
                this._activeItem.transform.localRotation = Quaternion.identity;
                
                this._ghost.Initialize(this._activeItem);
            }
        }

        public Vector2Int GetCurrentSize()
        {
            if (this._currentItemData == null)
            {
                return new Vector2Int(1, 1);
            }

            if (this._rotationStep % 2 == 1)
            {
                return new Vector2Int(this._currentItemData.size.y, this._currentItemData.size.x);
            }
            
            return this._currentItemData.size;
        }

        public void RotateItem()
        {
            this._rotationStep = (this._rotationStep + 1) % 4;
            this._activeItem.transform.rotation = Quaternion.Euler(0, this._rotationStep * 90f, 0);
        }

        public void UpdateGhostPositionAndColor(Vector3 position, bool isValid)
        {
            this._activeItem.transform.position = position;
            this._ghost.UpdateGhostState(isValid);
        }

        public void CleanupGhost()
        {
            if (this._activeItem != null)
            {
                Destroy(this._activeItem);
                this._activeItem = null;
            }
        }
    }
}