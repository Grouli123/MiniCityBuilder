using Domain.Gameplay.Models;
using ContractsInterfaces.DomainGameplay;
using System;

namespace Application.Services
{
    public sealed class BuildingUpgradeService : IBuildingUpgradeService
    {
        readonly CityGrid grid;
        readonly IBuildingCatalog catalog;
        readonly IWalletService wallet;

        public BuildingUpgradeService(CityGrid grid, IBuildingCatalog catalog, IWalletService wallet)
        {
            this.grid = grid;
            this.catalog = catalog;
            this.wallet = wallet;
        }

        public bool TryUpgrade(int buildingId, out int newLevel)
        {
            newLevel = 0;

            var inst = grid.Get(buildingId);
            if (inst == null) return false;

            if (!catalog.TryGetDefinition(inst.Type, out var def)) return false;

            var currentLevel = inst.Level;
            if (currentLevel >= def.Levels.Count) return false;

            var upgradeCost = def.Levels[currentLevel].UpgradeCostGold;
            if (!wallet.TrySpend(upgradeCost)) return false;

            inst.Upgrade();
            newLevel = inst.Level;
            return true;
        }
    }
}