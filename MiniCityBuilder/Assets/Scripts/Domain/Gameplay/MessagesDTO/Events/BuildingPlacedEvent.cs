namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Событие: здание успешно установлено.</summary>
    public readonly struct BuildingPlacedEvent
    {
        public readonly int BuildingId;
        public readonly int Type; 
        public readonly int X;
        public readonly int Y;
        public readonly int Rotation90;

        public BuildingPlacedEvent(int buildingId, int type, int x, int y, int rotation90)
        {
            BuildingId = buildingId;
            Type = type;
            X = x;
            Y = y;
            Rotation90 = rotation90;
        }
    }
}