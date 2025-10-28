using System;

namespace Application.Services
{
    public sealed class WalletService : IWalletService
    {
        public int Balance { get; private set; }
        public event Action<int>? OnBalanceChanged;

        public WalletService(int startGold = 500) => Set(startGold);

        public void Add(int amount)
        {
            if (amount <= 0) return;
            Balance += amount;
            OnBalanceChanged?.Invoke(Balance);
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0) return true;
            if (Balance < amount) return false;
            Balance -= amount;
            OnBalanceChanged?.Invoke(Balance);
            return true;
        }

        private void Set(int value)
        {
            Balance = Math.Max(0, value);
            OnBalanceChanged?.Invoke(Balance);
        }
    }
}