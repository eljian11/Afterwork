using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Room
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

            CleanupGhost();

            if (this._currentRoomItemData != null && this._currentRoomItemData.prefab != null)
            {
                this._activeItem = Instantiate(this._currentRoomItemData.prefab);
                this._ghost.Initialize(this._activeItem);
            }
        }

        public Vector2Int GetCurrentSize()
        {
            if (this._currentRoomItemData == null) return new Vector2Int(1, 1);

            // Swap dimensions on 90-degree and 270-degree rotations
            if (this._rotationStep % 2 == 1)
            {
                return new Vector2Int(this._currentRoomItemData.size.y, this._currentRoomItemData.size.x);
            }

            return this._currentRoomItemData.size;
        }
        
        public Quaternion GetCurrentRotation()
        {
            return Quaternion.Euler(0, this._rotationStep * 90f, 0);
        }

        public void RotateItem()
        {
            this._rotationStep = (this._rotationStep + 1) % 4;
        }

        public void UpdateGhostPositionAndColor(Vector3 bottomLeftWorldPos, bool isValid)
        {
            if (this._activeItem == null || this._currentRoomItemData == null) return;

            Vector2Int currentSize = GetCurrentSize();
            Quaternion currentRot = GetCurrentRotation();

            // Standard center offset based on grid size
            Vector3 gridCenterOffset = new Vector3(currentSize.x * 0.5f, 0f, currentSize.y * 0.5f);

            this._activeItem.transform.position = bottomLeftWorldPos + gridCenterOffset;
            this._activeItem.transform.rotation = currentRot;

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