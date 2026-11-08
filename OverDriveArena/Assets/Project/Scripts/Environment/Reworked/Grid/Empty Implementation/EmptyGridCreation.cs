using System.Collections.Generic;
using Project.Scripts.Environment.Reworked.Grid;
using UnityEngine;

namespace Project.Scripts.Room.Reworked
{
    public sealed class EmptyGridCreation : IGridCreation
    {
        public Dictionary<Vector2Int, CellData> CreateGrid()
        {
            return new Dictionary<Vector2Int, CellData>();
        }
    }
}