using Domain.Gameplay.Models;
using Infrastructure.Input;
using Infrastructure.Factories;
using Presentation.Gameplay.Adapters;   
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    /// <summary>Следит за выбранным типом, обновляет ghost и ставит здание по клику.</summary>
    public sealed class PlacementPresenter : IPostStartable, ITickable
    {
        readonly CityGrid grid;
        readonly UnityGridWorldAdapter converter;   
        readonly PlacementInputAdapter input;
        readonly BuildingCatalogRepositoryAdapter catalog;    
        readonly BuildingFactory factory;
        readonly BuildingGhostView ghost;

        BuildingType currentType;
        int lastIndex = -1;

        [Inject]
        public PlacementPresenter(
            CityGrid grid,
            UnityGridWorldAdapter converter,
            PlacementInputAdapter input,
            BuildingCatalogRepositoryAdapter catalog,
            BuildingFactory factory,
            BuildingGhostView ghost)
        {
            this.grid      = grid;
            this.converter = converter;
            this.input     = input;
            this.catalog   = catalog;
            this.factory   = factory;
            this.ghost     = ghost;
        }

        public void PostStart() => UpdateGhostByIndex(force: true);

        public void Tick()
        {
            UpdateGhostByIndex();

            if (!input.TryGetMouseRay(out var ray)) return;

            var plane = new Plane(Vector3.up, Vector3.zero);
            if (!plane.Raycast(ray, out var d)) return;

            var hit  = ray.GetPoint(d);
            var cell = converter.ToCell(hit);                

            ghost.SetWorldCenter(converter.ToWorldCenter(cell));

            if (input.LeftClickDown && grid.IsInside(cell) && !grid.IsOccupied(cell))
            {
                var pos = converter.ToWorldCenter(cell);
                var go  = factory.Create(currentType, pos, Quaternion.identity);

                var id = grid.All.Count + 1;
                var instance = new BuildingInstance(id, currentType, new GridPosition(cell.X, cell.Y), 1);
                if (grid.TryPlace(instance))
                {
                    var view = go.GetComponent<Presentation.Gameplay.Views.BuildingView>();
                    if (view != null)
                    {
                        view.BindInstanceId(id);
                        view.ApplyLevel(instance.Level);
                    }
                }
            }
        }

        private void UpdateGhostByIndex(bool force = false)
        {
            var idx = input.SelectedTypeIndex;
            if (!force && idx == lastIndex) return;
            lastIndex = idx;

            if (catalog.TryGetTypeByIndex(idx, out var type) &&
                catalog.TryGetViewPrefab(type, out var prefab))
            {
                currentType = type;
                ghost.SetPrefab(prefab);
            }
        }
    }
}