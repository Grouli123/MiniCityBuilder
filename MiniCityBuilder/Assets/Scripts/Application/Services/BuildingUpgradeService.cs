using Domain.Gameplay.Models;
using ContractsInterfaces.DomainGameplay;   

namespace Application.Services
{
    /// <summary>Простая логика апгрейда: без экономики, только проверка максимального уровня.</summary>
    public sealed class BuildingUpgradeService : IBuildingUpgradeService
    {
        private readonly CityGrid grid;
        private readonly IBuildingCatalog catalog;   // <-- порт, не адаптер

        public BuildingUpgradeService(CityGrid grid, IBuildingCatalog catalog)
        {
            this.grid = grid;
            this.catalog = catalog;
        }

        public bool TryUpgrade(int buildingId, out int newLevel)
        {
            newLevel = 0;

            var inst = grid.Get(buildingId);
            if (inst == null) return false;

            if (!catalog.TryGetDefinition(inst.Type, out var def)) return false;
            var maxLvl = def.Levels.Count;

            if (inst.Level >= maxLvl) return false;

            inst.Upgrade();
            newLevel = inst.Level;
            return true;
        }
    }
}