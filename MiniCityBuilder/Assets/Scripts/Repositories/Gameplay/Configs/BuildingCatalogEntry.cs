using System;
using System.Collections.Generic;
using Domain.Gameplay.Models; 
using UnityEngine;

namespace Repositories.Gameplay.Configs
{
    /// <summary>Элемент каталога зданий для инспектора.</summary>
    [Serializable]
    public sealed class BuildingCatalogEntry
    {
        public BuildingType type = BuildingType.House;
        public int placeCostGold = 50;
        public List<BuildingLevelConfig> levels = new()
        {
            new BuildingLevelConfig { level = 1, upgradeCostGold = 100, incomePerTickGold = 2 },
            new BuildingLevelConfig { level = 2, upgradeCostGold = 150, incomePerTickGold = 4 },
        };

        public GameObject viewPrefab; 
    }
}