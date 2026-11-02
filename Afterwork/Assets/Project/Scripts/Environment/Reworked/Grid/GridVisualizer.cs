using System.Collections.Generic;
using Project.Scripts.Room.Reworked;
using UnityEngine;

namespace Project.Scripts.Environment.Reworked.Grid
{
    public sealed class GridVisualizer : IGridVisualizer
    {
        private MeshFilter _meshFilter;
        private Mesh _gridMesh;
        private Dictionary<Vector2Int, CellData> _currentGrid;

        public void Initialize(Dictionary<Vector2Int, CellData> grid, float tileSize, MeshFilter meshFilter)
        {
            this._meshFilter = meshFilter;

            this._gridMesh = new Mesh();
            this._gridMesh.name = "GeneratedGridOverlayMesh";
            this._meshFilter.mesh = this._gridMesh;

            this._currentGrid = grid;

            GenerateGridMesh(grid, tileSize);
        }

        private void GenerateGridMesh(Dictionary<Vector2Int, CellData> grid, float tileSize)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var colors = new List<Color>();

            var index = 0;
            foreach (var kvp in grid)
            {
                var cellPos = kvp.Key;
                var cellData = kvp.Value;

                var x = cellPos.x * tileSize;
                var z = cellPos.y * tileSize;

                var p0 = new Vector3(x, 0.01f, z);
                var p1 = new Vector3(x + tileSize, 0.01f, z);
                var p2 = new Vector3(x + tileSize, 0.01f, z + tileSize);
                var p3 = new Vector3(x, 0.01f, z + tileSize);

                vertices.Add(p0);
                vertices.Add(p1);
                vertices.Add(p2);
                vertices.Add(p3);

                var cellColor = cellData.IsOccupied
                    ? new Color(1f, 0.6f, 0.6f, 0.4f)
                    : new Color(0.7f, 1f, 0.7f, 0.4f);

                colors.Add(cellColor);
                colors.Add(cellColor);
                colors.Add(cellColor);
                colors.Add(cellColor);

                // Two triangles forming the quad
                triangles.Add(index + 0);
                triangles.Add(index + 2);
                triangles.Add(index + 1);

                triangles.Add(index + 0);
                triangles.Add(index + 3);
                triangles.Add(index + 2);

                index += 4;
            }

            this._gridMesh.Clear();
            this._gridMesh.SetVertices(vertices);
            this._gridMesh.SetColors(colors);
            this._gridMesh.SetTriangles(triangles, 0);
            this._gridMesh.RecalculateNormals();
        }

        public void SetVisibility(bool isVisible)
        {
            this._meshFilter.gameObject.SetActive(isVisible);
        }

        public void UpdateGrid()
        {
            GenerateGridMesh(this._currentGrid, 1f);
        }
    }
}