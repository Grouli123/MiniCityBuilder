using Domain.Gameplay.Models;
using Infrastructure.Grid;
using Infrastructure.Input;
using Infrastructure.Factories;
using Presentation.Gameplay.Views;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Repositories.Gameplay.Configs;
using Presentation.Gameplay.Adapters;
using Application.Services;
using ContractsInterfaces.DomainGameplay; 

namespace Installers.Gameplay
{
    public sealed class PresentationGameplayInstaller : LifetimeScope
    {
        [Header("Grid")]
        [SerializeField] int width = 32;
        [SerializeField] int height = 32;
        [SerializeField] float cellSize = 1f;
        [SerializeField] Vector3 gridOrigin = Vector3.zero;

        [Header("Scene References")]
        [SerializeField] PlacementInputAdapter inputAdapter;
        [SerializeField] GridHighlightView gridHighlightView;
        [SerializeField] BuildingGhostView buildingGhostView;

        [Header("Factory")]
        [SerializeField] BuildingCatalogRepository buildingCatalog; 
        [SerializeField] Transform buildingsRoot;
        
        protected override void Configure(IContainerBuilder builder)
        {
            var grid = new CityGrid(width, height, cellSize);
            builder.RegisterInstance(grid);

            var converter = new GridWorldConverter(grid, gridOrigin);
            builder.RegisterInstance(converter);
            builder.RegisterInstance(new UnityGridWorldAdapter(converter, cellSize));

            var adapterRepo = new BuildingCatalogRepositoryAdapter(buildingCatalog);
            builder.RegisterInstance(adapterRepo)
                .As<IBuildingCatalog>()
                .AsSelf();

            builder.RegisterInstance(buildingsRoot);
            builder.Register<BuildingFactory>(Lifetime.Singleton);

            builder.RegisterComponent(inputAdapter);
            builder.RegisterComponent(gridHighlightView);
            builder.RegisterComponent(buildingGhostView);

            builder.Register<BuildingUpgradeService>(Lifetime.Singleton)
                .As<IBuildingUpgradeService>();

            builder.RegisterEntryPoint<Presentation.Gameplay.Presenters.GridHoverPresenter>();
            builder.RegisterEntryPoint<Presentation.Gameplay.Presenters.PlacementPresenter>();
            builder.RegisterEntryPoint<Presentation.Gameplay.Presenters.BuildingUpgradePresenter>();
        }
    }
}