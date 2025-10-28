using System.Collections.Generic;

namespace Domain.Gameplay.Models
{
    /// <summary>Состояние сетки: границы, занятые клетки и размещённые здания.</summary>
    public sealed class CityGrid
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }

        private readonly HashSet<GridPosition> _occupied = new();
        private readonly Dictionary<int, BuildingInstance> _byId = new();

        public CityGrid(int width, int height, float cellSize = 1f)
        {
            Width = width;
            Height = height;
            CellSize = cellSize <= 0f ? 1f : cellSize;
        }

        public bool IsInside(GridPosition p) => p.X >= 0 && p.Y >= 0 && p.X < Width && p.Y < Height;
        public bool InBounds(GridPosition p) => IsInside(p);
        public bool IsFree(GridPosition p) => !_occupied.Contains(p);
        public bool IsOccupied(GridPosition p) => _occupied.Contains(p);

        public IReadOnlyDictionary<int, BuildingInstance> All => _byId;

        public bool TryPlace(BuildingInstance instance)
        {
            if (!IsInside(instance.Position) || !IsFree(instance.Position)) return false;

            _byId[instance.Id] = instance;
            _occupied.Add(instance.Position);
            return true;
        }

        public bool TryMove(int id, GridPosition newPos)
        {
            if (!_byId.TryGetValue(id, out var inst)) return false;
            if (!IsInside(newPos) || !IsFree(newPos)) return false;

            _occupied.Remove(inst.Position);
            inst.MoveTo(newPos);
            _occupied.Add(inst.Position);
            return true;
        }

        public bool Remove(int id)
        {
            if (!_byId.TryGetValue(id, out var inst)) return false;
            _occupied.Remove(inst.Position);
            return _byId.Remove(id);
        }

        public bool TryFree(GridPosition p)
        {
            if (!_occupied.Contains(p)) return false;
            _occupied.Remove(p);
            return true;
        }

        public BuildingInstance? Get(int id) => _byId.TryGetValue(id, out var v) ? v : null;
    }
}