using Domain.Gameplay.Models;

namespace ContractsInterfaces.DomainGameplay
{
    /// <summary>Порт для получения определения типа здания (стоимости и уровней).</summary>
    public interface IBuildingCatalog
    {
        bool TryGetDefinition(BuildingType type, out BuildingDefinition definition);
    }
}