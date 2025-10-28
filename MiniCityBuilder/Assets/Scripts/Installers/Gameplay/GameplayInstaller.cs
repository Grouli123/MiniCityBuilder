using ContractsInterfaces.DomainGameplay;
using Domain.Gameplay.Models;
using Infrastructure.Grid;
using Infrastructure.Input;
using Presentation.Gameplay.Adapters;
using Presentation.Gameplay.Views;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Installers.Gameplay
{
    public sealed class GameplayInstaller : LifetimeScope
    {
        [Header("Grid")]
        [SerializeField] private int width = 32;
        [SerializeField] private int height = 32;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector3 gridOrigin = Vector3.zero;

        [Header("References")]
        [SerializeField] private PlacementInputAdapter inputAdapter;
        [SerializeField] private GridHighlightView gridHighlightView;

        protected override void Configure(IContainerBuilder builder)
        {
            // Domain model
            var grid = new CityGrid(width, height, cellSize);
            builder.RegisterInstance(grid);

            // Port + implementation (Infrastructure)
            var impl = new GridWorldConverter(grid, gridOrigin);
            builder.RegisterInstance<IGridWorldConverter>(impl);

            // Adapter for Presentation
            builder.RegisterInstance(new UnityGridWorldAdapter(impl, cellSize));

            // Scene components
            builder.RegisterComponent(inputAdapter);
            builder.RegisterComponent(gridHighlightView);

            // Presenter entrypoint
            builder.RegisterEntryPoint<Presentation.Gameplay.Presenters.GridHoverPresenter>();
        }
    }
}