using VContainer.Unity;
using UnityEngine;
using Application.Services;

namespace Presentation.Gameplay.Presenters
{
    /// <summary>Запускает начисление дохода раз в N секунд.</summary>
    public sealed class IncomeTickRunner : ITickable
    {
        private readonly IncomeTickService service;

        public IncomeTickRunner(IncomeTickService service) => this.service = service;

        public void Tick() => service.Tick(Time.deltaTime);
    }
}