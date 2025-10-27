namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Запрос на апгрейд здания.</summary>
    public readonly struct UpgradeBuildingRequest
    {
        public readonly int BuildingId;
        public UpgradeBuildingRequest(int buildingId) => BuildingId = buildingId;
    }
}