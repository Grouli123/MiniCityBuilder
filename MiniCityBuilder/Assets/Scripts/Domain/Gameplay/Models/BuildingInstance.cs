namespace Domain.Gameplay.Models
{
    /// <summary>Экземпляр здания на карте (чистая модель, без Unity API).</summary>
    public sealed class BuildingInstance
    {
        public int Id { get; }
        public BuildingType Type { get; }
        public int Level { get; private set; }
        public GridPosition Position { get; private set; }
        public int Rotation90 { get; private set; } 

        public BuildingInstance(int id, BuildingType type, GridPosition pos, int rotation90, int level = 1)
        {
            Id = id;
            Type = type;
            Position = pos;
            Rotation90 = ((rotation90 % 360) + 360) % 360; 
            Level = level;
        }

        public void MoveTo(GridPosition newPos) => Position = newPos;

        public void RotateCW() => Rotation90 = (Rotation90 + 90) % 360;

        public void SetRotation(int rotation90) =>
            Rotation90 = ((rotation90 % 360) + 360) % 360;

        public void Upgrade() => Level++;
    }
}