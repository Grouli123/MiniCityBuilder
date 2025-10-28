using UnityEngine;
using VContainer;
using VContainer.Unity;
using Repositories.Gameplay.Configs;

namespace Infrastructure.Factories
{
    public sealed class BuildingFactoryInstaller : LifetimeScope
    {
        [Header("References")]
        [SerializeField] private BuildingCatalogRepository buildingCatalog;
        [SerializeField] private Transform buildingsRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new BuildingCatalogRepositoryAdapter(buildingCatalog)).AsSelf();
            builder.RegisterInstance(buildingsRoot);
            builder.Register<BuildingFactory>(Lifetime.Singleton);
        }
    }
}