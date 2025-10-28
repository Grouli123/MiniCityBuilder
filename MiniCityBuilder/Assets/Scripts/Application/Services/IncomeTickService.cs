using Domain.Gameplay.Models;
using UnityEngine;

namespace Application.Services
{
    public sealed class IncomeTickService
    {
        readonly CityGrid _grid;
        readonly IWalletService _wallet;
        readonly float _interval;
        float _timer;

        public IncomeTickService(CityGrid grid, IWalletService wallet, float interval = 5f)
        {
            _grid = grid;
            _wallet = wallet;
            _interval = interval;
        }

        public void Tick(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer < _interval) return;
            _timer = 0f;

            int income = 0;

            foreach (var inst in _grid.All.Values)
                income += inst.Level * 5;

            _wallet.Add(income);
        }
    }
}