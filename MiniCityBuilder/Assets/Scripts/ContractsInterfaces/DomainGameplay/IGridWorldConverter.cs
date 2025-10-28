namespace ContractsInterfaces.DomainGameplay
{
    using Domain.Gameplay.Models;

    /// <summary>
    /// Интерфейс конвертации координат сетки в мировые (без Unity API).
    /// Возвращает float[], которые потом в инфраструктуре конвертируются в Vector3.
    /// </summary>
    public interface IGridWorldConverter
    {
        float[] ToWorldCenter(GridPosition cell);
        GridPosition ToCell(float x, float z);
    }
}