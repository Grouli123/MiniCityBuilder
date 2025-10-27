using Domain.Gameplay.Models;
using UnityEngine;

namespace Infrastructure.Factories
{
    /// <summary>Фабрика для спавна зданий на сцене по данным из каталога.</summary>
    public sealed class BuildingFactory
    {
        private readonly BuildingCatalogRepositoryAdapter _catalogAdapter;
        private readonly Transform _parentContainer;

        public BuildingFactory(BuildingCatalogRepositoryAdapter catalogAdapter, Transform parentContainer)
        {
            _catalogAdapter = catalogAdapter;
            _parentContainer = parentContainer;
        }

        /// <summary>Создаёт экземпляр здания указанного типа на позиции.</summary>
        public GameObject Create(BuildingType type, Vector3 position, Quaternion rotation)
        {
            if (_catalogAdapter.TryGetViewPrefab(type, out var prefab) && prefab != null)
            {
                return Object.Instantiate(prefab, position, rotation, _parentContainer);
            }

            Debug.LogError($"[BuildingFactory] Prefab not found for type {type}");
            return null;
        }
    }
}