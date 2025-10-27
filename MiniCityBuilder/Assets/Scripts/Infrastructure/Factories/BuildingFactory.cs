using Domain.Gameplay.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity; 

namespace Infrastructure.Factories
{
    /// <summary>Фабрика спавна зданий по данным каталога.</summary>
    public sealed class BuildingFactory
    {
        private readonly BuildingCatalogRepositoryAdapter _catalogAdapter;
        private readonly Transform _parentContainer;
        private readonly IObjectResolver _resolver;

        public BuildingFactory(
            BuildingCatalogRepositoryAdapter catalogAdapter,
            Transform parentContainer,
            IObjectResolver resolver)
        {
            _catalogAdapter = catalogAdapter;
            _parentContainer = parentContainer;
            _resolver = resolver;
        }

        /// <summary>Создать экземпляр здания указанного типа на позиции/повороте.</summary>
        public GameObject Create(BuildingType type, Vector3 position, Quaternion rotation)
        {
            if (!_catalogAdapter.TryGetViewPrefab(type, out var prefab) || prefab == null)
            {
                Debug.LogError($"[BuildingFactory] Prefab not found for type '{type}'.");
                return null!;
            }

            var go = Object.Instantiate(prefab, position, rotation, _parentContainer);

            _resolver.InjectGameObject(go);

            return go;
        }
    }
}