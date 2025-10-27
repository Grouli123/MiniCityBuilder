namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Причина неудачи при установке здания.</summary>
    public enum PlacementFailReason
    {
        OutOfBounds,
        Occupied,
        NotEnoughGold,
        Unknown
    }
}