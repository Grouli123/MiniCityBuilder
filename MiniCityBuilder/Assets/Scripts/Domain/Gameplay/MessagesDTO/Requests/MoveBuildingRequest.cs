namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Запрос на перемещение здания.</summary>
    public readonly struct MoveBuildingRequest
    {
        public readonly int BuildingId;
        public readonly int X;
        public readonly int Y;

        public MoveBuildingRequest(int buildingId, int x, int y)
        {
            BuildingId = buildingId;
            X = x;
            Y = y;
        }
    }
}