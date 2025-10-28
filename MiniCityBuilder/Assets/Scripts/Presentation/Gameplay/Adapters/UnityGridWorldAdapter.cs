using Domain.Gameplay.Models;
using Infrastructure.Grid;     
using UnityEngine;

namespace Presentation.Gameplay.Adapters
{
    public sealed class UnityGridWorldAdapter
    {
        private readonly GridWorldConverter converter;
        public float CellSize { get; }

        public UnityGridWorldAdapter(GridWorldConverter converter, float cellSize)
        {
            this.converter = converter;
            CellSize = cellSize;
        }

        public Vector3 ToWorldCenter(GridPosition cell) => converter.ToWorldCenter(cell);
        public GridPosition ToCell(Vector3 world) => converter.ToCell(world);
    }
}