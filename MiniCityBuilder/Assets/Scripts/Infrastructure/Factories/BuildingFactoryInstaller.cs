using UnityEngine;
using VContainer;
using VContainer.Unity;
using Repositories.Gameplay.Configs;

namespace Infrastructure.Factories
{
    /// <summary>
    /// Инсталлер для регистрации BuildingFactory и её зависимостей.
    /// Подключается к сцене и создаёт фабрику зданий при старте.
    /// </summary>
    public sealed class BuildingFactoryInstaller : LifetimeScope
    {
        [Header("References")]
        [SerializeField] private BuildingCatalogRepository buildingCatalog; 
        [SerializeField] private Transform buildingsRoot; 

        protected override void Configure(IContainerBuilder builder)
        {
            var adapter = new BuildingCatalogRepositoryAdapter(buildingCatalog);
            
            builder.RegisterInstance(adapter).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(new BuildingFactory(adapter, buildingsRoot)).AsSelf().AsImplementedInterfaces();
        }
    }
}