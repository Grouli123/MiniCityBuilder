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
        [SerializeField] int width = 32;
        [SerializeField] int height = 32;
        [SerializeField] float cellSize = 1f;
        [SerializeField] Vector3 gridOrigin = Vector3.zero;
        [SerializeField] float incomeIntervalSeconds = 5f;

        [Header("Scene References")]
        [SerializeField] PlacementInputAdapter inputAdapter;
        [SerializeField] GridHighlightView gridHighlightView;
        [SerializeField] BuildingGhostView buildingGhostView;
        [SerializeField] WalletHUDView walletHudView;

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
            builder.RegisterInstance(adapterRepo).As<IBuildingCatalog>().AsSelf();

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