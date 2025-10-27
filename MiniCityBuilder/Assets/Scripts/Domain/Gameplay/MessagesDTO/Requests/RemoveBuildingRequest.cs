namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Запрос на удаление здания.</summary>
    public readonly struct RemoveBuildingRequest
    {
        public readonly int BuildingId;
        public RemoveBuildingRequest(int buildingId) => BuildingId = buildingId;
    }
}