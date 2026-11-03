using Project.Scripts.Environment.Reworked.Grid;
using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Room 
{
    public sealed class RoomController : MonoBehaviour, IRoomController
    {
        [Header("References")] 
        [SerializeField] private GridManager gridManager;
        [SerializeField] private RoomItemPlacementHandler placementHandler;
        [SerializeField] private LayerMask floorLayer;

        private RoomItemData _activeRoomItemData;
        private bool _isPlacing = false;
        
        private Camera _mainCamera;

        private void Awake()
        {
            this._mainCamera = Camera.main;
        }

        public void BeginPlacement(RoomItemData roomItemData)
        {
            if (roomItemData == null)
            {
                return;
            }

            this._activeRoomItemData = roomItemData;
            this._isPlacing = true;
            this.gridManager.IsEditMode(true);
            this.placementHandler.Initialize(roomItemData);
        }

        private void Update()
        {
            if (!this._isPlacing)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                this.placementHandler.RotateItem();
            }

            var ray = this._mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100f, this.floorLayer))
            {
                Vector2Int currentSize = this.placementHandler.GetCurrentSize();
                Quaternion rotation = this.placementHandler.GetCurrentRotation();

                Vector2Int originCell = this.gridManager.GridPlacementSystem.WorldToGridPosition(hit.point);

                bool canPlace = this.gridManager.GridPlacementSystem.IsAreaAvailable(originCell, currentSize);
                
                Vector3 bottomLeftPos = this.gridManager.GridPlacementSystem.CellToWorldPosition(originCell);

                this.placementHandler.UpdateGhostPositionAndColor(bottomLeftPos, canPlace);

                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    ConfirmPlacement(originCell, currentSize, rotation);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacement();
            }
        }

        private void ConfirmPlacement(Vector2Int originCell, Vector2Int size, Quaternion rotation)
        {
            var bottomLeftPos = this.gridManager.GridPlacementSystem.CellToWorldPosition(originCell);
    
            Vector3 gridCenterOffset = new Vector3(size.x * 0.5f, 0f, size.y * 0.5f);
            Vector3 spawnPosition = bottomLeftPos + gridCenterOffset;
    
            GameObject permanentFurniture = Instantiate(this._activeRoomItemData.prefab, spawnPosition, rotation);

            for (var x = 0; x < size.x; x++)
            {
                for (var z = 0; z < size.y; z++)
                {
                    var targetCell = new Vector2Int(originCell.x + x, originCell.y + z);
                    this.gridManager.GridPlacementSystem.OccupyCell(targetCell, permanentFurniture);
                }
            }
    
            this.gridManager.GridVisualizer.UpdateGrid();
            CancelPlacement();
        }

        private void CancelPlacement()
        {
            this._isPlacing = false;
            this.placementHandler.CleanupGhost();
            this.gridManager.IsEditMode(false);
        }
    }

    public interface IRoomController
    {
        void BeginPlacement(RoomItemData roomItemData);
    }
}