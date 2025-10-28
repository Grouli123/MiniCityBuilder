using System;

namespace Application.Services
{
    public interface IWalletService
    {
        int Balance { get; }
        event Action<int> OnBalanceChanged;
        void Add(int amount);
        bool TrySpend(int amount);
    }
}