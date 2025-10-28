using UnityEngine;
using Domain.Gameplay.Models;

namespace Infrastructure.Grid
{
    public sealed class GridWorldConverter
    {
        private readonly CityGrid grid;
        private readonly Vector3 origin;

        public GridWorldConverter(CityGrid grid, Vector3 origin)
        {
            this.grid = grid;
            this.origin = origin;
        }

        public Vector3 ToWorldCenter(GridPosition cell)
        {
            float x = origin.x + cell.X * grid.CellSize + grid.CellSize / 2f;
            float z = origin.z + cell.Y * grid.CellSize + grid.CellSize / 2f;
            return new Vector3(x, origin.y, z);
        }

        public GridPosition ToCell(Vector3 world)
        {
            int x = Mathf.FloorToInt((world.x - origin.x) / grid.CellSize);
            int y = Mathf.FloorToInt((world.z - origin.z) / grid.CellSize);
            return new GridPosition(x, y);
        }

        public Vector3 Origin => origin;
    }
}