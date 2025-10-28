using Domain.Gameplay.Models;

namespace Application.Services
{
    public interface IBuildingUpgradeService
    {
        /// <summary>Пытается апгрейдить здание; возвращает true, если уровень увеличен.</summary>
        bool TryUpgrade(int buildingId, out int newLevel);
    }
}