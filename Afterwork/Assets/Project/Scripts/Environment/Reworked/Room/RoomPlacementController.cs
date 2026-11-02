using Project.Scripts.Environment.Reworked.Room;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts.Room.Reworked
{
    public class RoomPlacementController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GridManager gridManager;

        [SerializeField] private RoomItemPlacementHandler placementHandler;
        [SerializeField] private LayerMask floorLayer;

        private RoomItemData _activeRoomItemData;
        private bool isPlacing = false;

        [FormerlySerializedAs("_testItemData")] [Header("Testing UI")] [SerializeField]
        private RoomItemData testRoomItemData;

        [SerializeField] private Button _beginPlacementButton;

        private void OnEnable()
        {
            if (this._beginPlacementButton != null)
                this._beginPlacementButton.onClick.AddListener(() => BeginPlacement(this.testRoomItemData));
        }

        private void OnDisable()
        {
            if (this._beginPlacementButton != null)
                this._beginPlacementButton.onClick.RemoveListener(() => BeginPlacement(this.testRoomItemData));
        }

        public void BeginPlacement(RoomItemData roomItemData)
        {
            if (roomItemData == null) return;

            this._activeRoomItemData = roomItemData;
            this.isPlacing = true;
            this.gridManager.IsEditMode(true);
            this.placementHandler.Initialize(roomItemData);
        }

        private void Update()
        {
            if (!this.isPlacing) return;

            if (Input.GetKeyDown(KeyCode.R)) this.placementHandler.RotateItem();

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100f, this.floorLayer))
            {
                var originCell = this.gridManager.GridPlacementSystem.WorldToGridPosition(hit.point);
                var currentSize = this.placementHandler.GetCurrentSize();

                var canPlace = this.gridManager.GridPlacementSystem.IsAreaAvailable(originCell, currentSize);
                var snapPosition = this.gridManager.GridPlacementSystem.CellToWorldPosition(originCell);

                this.placementHandler.UpdateGhostPositionAndColor(snapPosition, canPlace);

                if (Input.GetMouseButtonDown(0) && canPlace) ConfirmPlacement(originCell, currentSize);
            }

            if (Input.GetMouseButtonDown(1)) CancelPlacement();
        }

        private void ConfirmPlacement(Vector2Int originCell, Vector2Int size)
        {
            var spawnPosition = this.gridManager.GridPlacementSystem.CellToWorldPosition(originCell);
            var permanentFurniture = Instantiate(this._activeRoomItemData.prefab, spawnPosition,
                this.placementHandler.transform.rotation);

            for (var x = 0; x < size.x; x++)
            for (var z = 0; z < size.y; z++)
            {
                var targetCell = new Vector2Int(originCell.x + x, originCell.y + z);
                this.gridManager.GridPlacementSystem.OccupyCell(targetCell, permanentFurniture);
            }

            this.gridManager.GridVisualizer.UpdateGrid();
            CancelPlacement();
        }

        private void CancelPlacement()
        {
            this.isPlacing = false;
            this.placementHandler.CleanupGhost();
            this.gridManager.IsEditMode(false);
        }
    }
}