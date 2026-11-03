using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Room
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Room/Item Data")]
    public class RoomItemData : ScriptableObject
    {
        public string itemName;
        public Vector2Int size = new(1, 1);
        public GameObject prefab;
    }
}