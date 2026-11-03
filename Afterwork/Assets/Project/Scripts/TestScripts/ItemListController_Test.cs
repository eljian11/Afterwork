using Project.Scripts.Environment.Reworked.Room;
using Project.Scripts.Environment.Reworked.Room.Item;
using UnityEngine;
using VContainer;

namespace Project.Scripts.TestScripts
{
    public sealed class ItemListControllerTest : MonoBehaviour
    {
        [SerializeField] private RoomItemRegistry _roomItemRegistry;
        [SerializeField] private ItemUI _itemUIPrefab;
        [SerializeField] private Transform _container;
        
        private IRoomController _roomController;
        
        private void Awake()
        {
            if(this._roomController == null)
            {
                Debug.LogError("RoomController is not assigned in the inspector.");
            }
            
            if (this._roomItemRegistry == null)
            {
                Debug.LogError("RoomItemRegistry is not assigned in the inspector.");
                return;
            }

            foreach (var item in this._roomItemRegistry.RoomItems)
            {
                var itemUIInstance = Instantiate(this._itemUIPrefab, this._container);
                
                itemUIInstance.Initialize(item);
                
                itemUIInstance.OnItemSelected += (selectedItem) =>
                {
                    this._roomController.BeginPlacement(selectedItem);
                };
                
                Debug.Log($"Item Name: {item.itemName}, Size: {item.size}, Prefab: {item.prefab}");
            }
        }
    }
}
