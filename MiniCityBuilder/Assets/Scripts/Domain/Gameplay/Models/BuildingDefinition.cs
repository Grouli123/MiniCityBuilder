using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Gameplay.Models
{
    /// <summary>Описание типа здания (каталог, иммутабельно).</summary>
    public sealed class BuildingDefinition
    {
        public BuildingType Type { get; }
        public int PlaceCostGold { get; }
        public IReadOnlyList<BuildingLevelInfo> Levels { get; }

        public BuildingDefinition(BuildingType type, int placeCostGold, IReadOnlyList<BuildingLevelInfo> levels)
        {
            Type = type;
            PlaceCostGold = placeCostGold;
            Levels = levels is ReadOnlyCollection<BuildingLevelInfo> ro ? ro : new ReadOnlyCollection<BuildingLevelInfo>(new List<BuildingLevelInfo>(levels));
        }
    }
}