using ContractsInterfaces.DomainGameplay;  
using Domain.Gameplay.Models;
using Repositories.Gameplay.Configs;          
using UnityEngine;

namespace Infrastructure.Factories
{
    /// <summary>Адаптер ScriptableObject-каталога под IBuildingCatalog.</summary>
    public sealed class BuildingCatalogRepositoryAdapter : IBuildingCatalog
    {
        private readonly BuildingCatalogRepository _asset;

        public BuildingCatalogRepositoryAdapter(BuildingCatalogRepository asset)
        {
            _asset = asset;
        }

        public bool TryGetDefinition(BuildingType type, out BuildingDefinition definition)
        {
            if (_asset.TryGet(type, out var def, out _))
            {
                definition = def!;     
                return true;
            }

            definition = default!;
            return false;
        }

        public bool TryGetViewPrefab(BuildingType type, out GameObject prefab)
        {
            if (_asset.TryGet(type, out _, out var p) && p != null)
            {
                prefab = p;
                return true;
            }

            prefab = null!;
            return false;
        }
        
        public bool TryGetTypeByIndex(int index, out BuildingType type) =>
            _asset.TryGetTypeByIndex(index, out type);
    }
}