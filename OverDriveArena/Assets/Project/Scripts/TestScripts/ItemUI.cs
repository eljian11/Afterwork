using System;
using Project.Scripts.Environment.Reworked.Room;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.TestScripts
{
    public sealed class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private Button _itemButton;
        [SerializeField] private RoomItemData _itemData;
        
        public Action<RoomItemData> OnItemSelected;

        private void OnEnable()
        {
            if (this._itemButton != null)
            {
                this._itemButton.onClick.AddListener(OnItemButtonClicked);
            }
        }
        
        private void OnDisable()
        {
            if (this._itemButton != null)
            {
                this._itemButton.onClick.RemoveListener(OnItemButtonClicked);
            }
        }

        private void OnItemButtonClicked()
        {
            this.OnItemSelected?.Invoke(this._itemData);
        }

        public void Initialize(RoomItemData itemData)
        {
            if (itemData == null)
            {
                Debug.LogError("ItemData is null.");
                return;
            }
            
            this._itemData = itemData;

            if (this._itemNameText != null)
            {
                this._itemNameText.text = itemData.itemName;
            }
        }
    }
}