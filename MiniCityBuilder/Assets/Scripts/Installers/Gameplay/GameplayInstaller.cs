using Domain.Gameplay.Models;
using Infrastructure.Grid;
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

        protected override void Configure(IContainerBuilder builder)
        {
            var grid = new CityGrid(width, height, cellSize);
            builder.RegisterInstance(grid);

            var converter = new GridWorldConverter(grid, gridOrigin);
            builder.RegisterInstance(converter);
        }
    }
}