using Domain.Gameplay.Models;
using UnityEngine;

namespace Infrastructure.Grid
{
    /// <summary>
    /// Конвертация координат между сеткой (GridPosition) и миром (Vector3) на плоскости Y=0.
    /// </summary>
    public sealed class GridWorldConverter
    {
        private readonly CityGrid grid;
        private readonly Vector3 origin;

        public GridWorldConverter(CityGrid grid, Vector3 origin)
        {
            this.grid = grid;
            this.origin = origin;
        }

        public Vector3 ToWorldCenter(in GridPosition cell)
        {
            var s = grid.CellSize;
            return new Vector3((cell.X + 0.5f) * s, 0f, (cell.Y + 0.5f) * s) + origin;
        }

        public GridPosition ToCell(in Vector3 world)
        {
            var s = grid.CellSize;
            var local = world - origin;
            int x = Mathf.FloorToInt(local.x / s);
            int y = Mathf.FloorToInt(local.z / s);
            return new GridPosition(x, y);
        }
    }
}