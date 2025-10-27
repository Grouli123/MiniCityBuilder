namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Событие: установка здания не удалась.</summary>
    public readonly struct PlacementFailedEvent
    {
        public readonly int Type; 
        public readonly int X;
        public readonly int Y;
        public readonly PlacementFailReason Reason;

        public PlacementFailedEvent(int type, int x, int y, PlacementFailReason reason)
        {
            Type = type;
            X = x;
            Y = y;
            Reason = reason;
        }
    }
}