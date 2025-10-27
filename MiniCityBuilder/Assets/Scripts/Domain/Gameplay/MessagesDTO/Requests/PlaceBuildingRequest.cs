namespace Domain.Gameplay.MessagesDTO
{
    /// <summary>Запрос на установку здания.</summary>
    public readonly struct PlaceBuildingRequest
    {
        public readonly int Type; 
        public readonly int X;
        public readonly int Y;
        public readonly int Rotation90;

        public PlaceBuildingRequest(int type, int x, int y, int rotation90 = 0)
        {
            Type = type;
            X = x;
            Y = y;
            Rotation90 = rotation90;
        }
    }
}