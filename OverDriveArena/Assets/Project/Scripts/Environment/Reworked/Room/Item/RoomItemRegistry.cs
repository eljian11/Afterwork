using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Room.Item
{
    public class RoomItemRegistry : MonoBehaviour
    {
        [SerializeField]
        private List<RoomItemData> _roomItems = new();
        
        public List<RoomItemData> RoomItems => _roomItems;
    }
}