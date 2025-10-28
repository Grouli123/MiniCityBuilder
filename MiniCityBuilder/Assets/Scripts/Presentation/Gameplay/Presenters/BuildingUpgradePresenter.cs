using System.Linq;
using Application.Services;
using Domain.Gameplay.Models;
using Infrastructure.Input;
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    /// <summary>Выбирает здание под курсором и апгрейдит (ПКМ или U).</summary>
    public sealed class BuildingUpgradePresenter : ITickable
    {
        readonly PlacementInputAdapter input;
        readonly IBuildingUpgradeService upgradeService;
        readonly Camera cam;

        [Inject]
        public BuildingUpgradePresenter(
            PlacementInputAdapter input,
            IBuildingUpgradeService upgradeService)
        {
            this.input = input;
            this.upgradeService = upgradeService;
            cam = Camera.main;
        }

        public void Tick()
        {
            if (!input.RightClickDown && !input.UpgradePressed)
                return;

            if (cam == null) return;

            var ray = cam.ScreenPointToRay(input.MousePosition);
            if (!Physics.Raycast(ray, out var hit, 200f))
                return;

            var view = hit.collider.GetComponentInParent<BuildingView>();
            if (view == null || view.InstanceId < 0)
                return;

            if (upgradeService.TryUpgrade(view.InstanceId, out var newLevel))
            {
                view.ApplyLevel(newLevel);
            }
        }
    }
}