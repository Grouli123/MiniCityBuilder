using Domain.Gameplay.Models;
using Infrastructure.Grid;
using Infrastructure.Input;
using Presentation.Gameplay.Adapters;
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
            var grid = new CityGrid(width, height, cellSize);
            builder.RegisterInstance(grid);

            var converter = new GridWorldConverter(grid, gridOrigin);
            builder.RegisterInstance(converter);

            builder.RegisterInstance(new UnityGridWorldAdapter(converter, cellSize));

            builder.RegisterComponent(inputAdapter);
            builder.RegisterComponent(gridHighlightView);

            builder.RegisterEntryPoint<Presentation.Gameplay.Presenters.GridHoverPresenter>();
        }
    }
}