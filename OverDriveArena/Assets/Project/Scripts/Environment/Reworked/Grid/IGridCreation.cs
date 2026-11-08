using System.Collections.Generic;
using Project.Scripts.Room.Reworked;
using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Grid
{
    public interface IGridCreation
    {
        Dictionary<Vector2Int, CellData> CreateGrid();
    }
}