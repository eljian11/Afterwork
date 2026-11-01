using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Room/Item Data")]
    public class RoomItemData : ScriptableObject
    {
        public string itemName;
        public Vector2Int size = new(1, 1); // Specific width and depth for this item
        public GameObject prefab; // The actual 3D model to spawn
    }
}