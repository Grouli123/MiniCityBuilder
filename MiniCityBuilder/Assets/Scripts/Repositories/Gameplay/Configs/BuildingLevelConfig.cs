using System;

namespace Repositories.Gameplay.Configs
{
    /// <summary>Конфиг уровня здания для хранения.</summary>
    [Serializable]
    public sealed class BuildingLevelConfig
    {
        public int level = 1;
        public int upgradeCostGold = 100;
        public int incomePerTickGold = 2;
    }
}