namespace Domain.Gameplay.Models
{
    /// <summary>Координата клетки сетки (целочисленная).</summary>
    public readonly struct GridPosition
    {
        public readonly int X;
        public readonly int Y;

        public GridPosition(int x, int y) { X = x; Y = y; }

        public override string ToString() => $"({X},{Y})";
        public override int GetHashCode() => (X * 397) ^ Y;
        public override bool Equals(object obj) => obj is GridPosition p && p.X == X && p.Y == Y;
        public static bool operator ==(GridPosition a, GridPosition b) => a.Equals(b);
        public static bool operator !=(GridPosition a, GridPosition b) => !a.Equals(b);
    }
}