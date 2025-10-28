using System.Collections.Generic;
using System.Linq;
using Domain.Gameplay.Models;
using Repositories.Gameplay.Configs;
using UnityEngine;

namespace Repositories.Gameplay.Configs
{
    /// <summary>
    /// Каталог типов зданий (ScriptableObject).
    /// Хранит сериализуемые конфиги и маппит их в иммутабельные Domain-модели.
    /// </summary>
    [CreateAssetMenu(fileName = "BuildingCatalog", menuName = "CityBuilder/Repositories/Building Catalog")]
    public sealed class BuildingCatalogRepository : ScriptableObject
    {
        [Header("Catalog")]
        [SerializeField] private List<BuildingCatalogEntry> entries = new();
        public IReadOnlyList<BuildingCatalogEntry> Entries => entries;
        /// <summary>Возвращает доменную дефиницию и префаб визуала.</summary>
        public bool TryGet(BuildingType type, out BuildingDefinition def, out GameObject prefab)
        {
            var e = entries.FirstOrDefault(x => x.type == type);
            if (e == null || e.viewPrefab == null)
            {
                def = null;
                prefab = null;
                return false;
            }

            var levels = e.levels.Select(l => new BuildingLevelInfo(l.level, l.upgradeCostGold, l.incomePerTickGold)).ToList();

            def = new BuildingDefinition(e.type, e.placeCostGold, levels);
            prefab = e.viewPrefab;
            return true;
        }
        
        public bool TryGetTypeByIndex(int index, out BuildingType type)
        {
            if (index >= 0 && index < entries.Count)
            {
                type = entries[index].type;
                return true;
            }
            type = default;
            return false;
        }
    }
}