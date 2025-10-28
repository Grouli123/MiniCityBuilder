using Domain.Gameplay.Models;
using Infrastructure.Input;
using Presentation.Gameplay.Adapters;
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public sealed class GridHoverPresenter : IPostStartable, ITickable
    {
        private readonly CityGrid _grid;
        private readonly UnityGridWorldAdapter _converter;
        private readonly PlacementInputAdapter _input;
        private readonly GridHighlightView _highlight;
        private readonly Plane _groundPlane;
        private readonly Vector3 _cellSizeVec;

        [Inject]
        public GridHoverPresenter(
            CityGrid grid,
            UnityGridWorldAdapter converter,
            PlacementInputAdapter input,
            GridHighlightView highlight)
        {
            _grid = grid;
            _converter = converter;
            _input = input;
            _highlight = highlight;

            _groundPlane = new Plane(Vector3.up, Vector3.zero);
            _cellSizeVec = new Vector3(_converter.CellSize, 0f, _converter.CellSize);
        }

        public void PostStart() { }

        public void Tick()
        {
            if (!_input.TryGetMouseRay(out var ray)) return;
            if (!_groundPlane.Raycast(ray, out var dist)) return;

            var hit = ray.GetPoint(dist);
            var cell = _converter.ToCell(hit);

            var worldCenter = _converter.ToWorldCenter(cell);
            var allowed = _grid.IsInside(cell) && _grid.IsFree(cell); // или IsOccupied==false

            _highlight.SetWorldTransform(worldCenter, _cellSizeVec, 0.01f);
            _highlight.SetAllowed(allowed);
        }
    }
}