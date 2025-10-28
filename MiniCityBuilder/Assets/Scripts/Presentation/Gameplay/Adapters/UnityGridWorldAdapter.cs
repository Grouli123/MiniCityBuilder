using ContractsInterfaces.DomainGameplay;
using Domain.Gameplay.Models;
using UnityEngine;

namespace Presentation.Gameplay.Adapters
{
    /// <summary>Адаптер над IGridWorldConverter для работы с Vector3 в Presentation.</summary>
    public sealed class UnityGridWorldAdapter
    {
        private readonly IGridWorldConverter _converter;
        private readonly float _cellSize;

        public UnityGridWorldAdapter(IGridWorldConverter converter, float cellSize)
        {
            _converter = converter;
            _cellSize = cellSize;
        }

        public float CellSize => _cellSize;

        public Vector3 ToWorldCenter(GridPosition cell)
        {
            var a = _converter.ToWorldCenter(cell); // float[2] => x,z
            return new Vector3(a[0], 0f, a[1]);
        }

        public GridPosition ToCell(Vector3 world) =>
            _converter.ToCell(world.x, world.z);
    }
}