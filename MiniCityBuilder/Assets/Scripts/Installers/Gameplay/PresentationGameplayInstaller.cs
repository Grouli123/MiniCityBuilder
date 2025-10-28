using Application.Services;
using ContractsInterfaces.DomainGameplay;      
using Domain.Gameplay.Models;
using Infrastructure.Factories;
using Infrastructure.Grid;
using Infrastructure.Input;
using Presentation.Gameplay.Adapters;
using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.Views;
using Presentation.HUD;
using Repositories.Gameplay.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers.Gameplay
{
    public sealed class PresentationGameplayInstaller : LifetimeScope
    {
        [Header("Grid")]
        [SerializeField] private int width = 32;
        [SerializeField] private int height = 32;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector3 gridOrigin = Vector3.zero;
        [SerializeField] private float incomeIntervalSeconds = 5f;

        [Header("Scene References")]
        [SerializeField] private PlacementInputAdapter inputAdapter;
        [SerializeField] private GridHighlightView gridHighlightView;
        [SerializeField] private BuildingGhostView buildingGhostView;
        [SerializeField] private WalletHUDView walletHudView;

        [Header("Factory")]
        [SerializeField] private BuildingCatalogRepository buildingCatalog;
        [SerializeField] private Transform buildingsRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            var grid = new CityGrid(width, height, cellSize);
            builder.RegisterInstance(grid);

            var converter = new GridWorldConverter(grid, gridOrigin);
            builder.RegisterInstance(converter);
            builder.RegisterInstance(new UnityGridWorldAdapter(converter, cellSize));

            var catalogAdapter = new BuildingCatalogRepositoryAdapter(buildingCatalog);
            builder.RegisterInstance(catalogAdapter)
                   .As<IBuildingCatalog>()
                   .AsSelf();

            builder.RegisterInstance(buildingsRoot);
            builder.Register<BuildingFactory>(Lifetime.Singleton);

            builder.Register<IWalletService>(_ => new WalletService(500), Lifetime.Singleton);
            builder.Register<IncomeTickService>(Lifetime.Singleton)
                   .WithParameter<float>(incomeIntervalSeconds);
            builder.RegisterEntryPoint<IncomeTickRunner>();

            builder.Register<BuildingUpgradeService>(Lifetime.Singleton)
                   .As<IBuildingUpgradeService>();

            builder.RegisterComponent(inputAdapter);
            builder.RegisterComponent(gridHighlightView);
            builder.RegisterComponent(buildingGhostView);
            builder.RegisterComponent(walletHudView);

            builder.RegisterEntryPoint<GridHoverPresenter>();
            builder.RegisterEntryPoint<PlacementPresenter>();
            builder.RegisterEntryPoint<BuildingUpgradePresenter>();
            builder.RegisterEntryPoint<WalletPresenter>();
        }
    }
}