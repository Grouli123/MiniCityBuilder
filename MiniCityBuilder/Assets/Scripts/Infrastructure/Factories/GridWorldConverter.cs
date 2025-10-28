using ContractsInterfaces.DomainGameplay;
using Domain.Gameplay.Models;
using UnityEngine;

namespace Infrastructure.Grid
{
    /// <summary>Конвертация Grid <-> World (Y=0). Реализация порта без утечки в Domain.</summary>
    public sealed class GridWorldConverter : IGridWorldConverter
    {
        private readonly CityGrid _grid;
        private readonly Vector3 _origin;

        public GridWorldConverter(CityGrid grid, Vector3 origin)
        {
            _grid   = grid;
            _origin = origin;
        }

        // реализация порта (без Vector3 наружу)
        public float[] ToWorldCenter(GridPosition cell)
        {
            var s = _grid.CellSize;
            return new[] { (cell.X + 0.5f) * s + _origin.x, (cell.Y + 0.5f) * s + _origin.z };
        }

        public GridPosition ToCell(float x, float z)
        {
            var s = _grid.CellSize;
            var lx = x - _origin.x;
            var lz = z - _origin.z;
            int cx = Mathf.FloorToInt(lx / s);
            int cy = Mathf.FloorToInt(lz / s);
            return new GridPosition(cx, cy);
        }

        // утилита для инфраструктуры/инсталлеров, если нужна
        public float CellSize => _grid.CellSize;
    }
}