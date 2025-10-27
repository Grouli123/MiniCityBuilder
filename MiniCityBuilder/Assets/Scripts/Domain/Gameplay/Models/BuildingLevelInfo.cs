namespace Domain.Gameplay.Models
{
    /// <summary>Данные уровня: стоимость апгрейда и доход (иммутабельная модель).</summary>
    public sealed class BuildingLevelInfo
    {
        public int Level { get; }
        public int UpgradeCostGold { get; }
        public int IncomePerTickGold { get; }

        public BuildingLevelInfo(int level, int upgradeCostGold, int incomePerTickGold)
        {
            Level = level;
            UpgradeCostGold = upgradeCostGold;
            IncomePerTickGold = incomePerTickGold;
        }

        /// <summary>Удобный With для копирования с изменением.</summary>
        public BuildingLevelInfo With(int? level = null, int? upgradeCostGold = null, int? incomePerTickGold = null)
            => new BuildingLevelInfo(
                level ?? Level,
                upgradeCostGold ?? UpgradeCostGold,
                incomePerTickGold ?? IncomePerTickGold);
    }
}