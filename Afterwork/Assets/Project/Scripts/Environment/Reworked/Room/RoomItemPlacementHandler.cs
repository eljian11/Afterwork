using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class RoomItemPlacementHandler : MonoBehaviour
    {
        [SerializeField] private ItemPlacementGhost _ghost = new();
        
        private RoomItemData _currentRoomItemData;
        private int _rotationStep = 0;
        private GameObject _activeItem;

        public void Initialize(RoomItemData data)
        {
            this._currentRoomItemData = data;
            this._rotationStep = 0;
            this.transform.rotation = Quaternion.identity;

            CleanupGhost();

            if (this._currentRoomItemData != null && this._currentRoomItemData.prefab != null)
            {
                this._activeItem = Instantiate(this._currentRoomItemData.prefab, this.transform);
                this._activeItem.transform.localPosition = Vector3.zero;
                this._activeItem.transform.localRotation = Quaternion.identity;
                
                this._ghost.Initialize(this._activeItem);
            }
        }

        public Vector2Int GetCurrentSize()
        {
            if (this._currentRoomItemData == null)
            {
                return new Vector2Int(1, 1);
            }

            if (this._rotationStep % 2 == 1)
            {
                return new Vector2Int(this._currentRoomItemData.size.y, this._currentRoomItemData.size.x);
            }
            
            return this._currentRoomItemData.size;
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