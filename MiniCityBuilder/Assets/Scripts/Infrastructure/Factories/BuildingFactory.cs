using Domain.Gameplay.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Factories
{
    /// <summary>Фабрика спавна зданий по данным каталога.</summary>
    public sealed class BuildingFactory
    {
        private readonly BuildingCatalogRepositoryAdapter catalog;
        private readonly Transform parent;
        private readonly IObjectResolver resolver;

        public BuildingFactory(
            BuildingCatalogRepositoryAdapter catalog,
            Transform parent,                     
            IObjectResolver resolver)             
        {
            this.catalog  = catalog;
            this.parent   = parent;
            this.resolver = resolver;
        }

        /// <summary>Создать экземпляр здания указанного типа на позиции/повороте.</summary>
        public GameObject Create(BuildingType type, Vector3 position, Quaternion rotation)
        {
            if (!catalog.TryGetViewPrefab(type, out var prefab) || prefab == null)
            {
                Debug.LogError($"[BuildingFactory] Prefab not found for type '{type}'.");
                return null!;
            }

            var go = Object.Instantiate(prefab, position, rotation, parent);

            resolver.InjectGameObject(go);

            return go;
        }
    }
}