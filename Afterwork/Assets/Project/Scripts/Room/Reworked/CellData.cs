using System;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    [Serializable]
    public sealed class CellData
    {
        public bool IsOccupied
        {
            get => this._isOccupied;
            set => this._isOccupied = value;
        }

        public Vector2Int Position
        {
            get => this._position;
            set => this._position = value;
        }

        private bool _isOccupied = false;
        private Vector2Int _position = Vector2Int.zero;
    }
}