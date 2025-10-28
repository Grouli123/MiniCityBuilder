using Domain.Gameplay.Models;
using Infrastructure.Grid;
using Infrastructure.Input;
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    /// <summary>Презентер подсветки клетки под курсором.</summary>
    public sealed class GridHoverPresenter : IPostStartable, ITickable
    {
        private readonly CityGrid grid;
        private readonly GridWorldConverter converter;
        private readonly PlacementInputAdapter input;
        private readonly GridHighlightView highlight;
        private readonly Plane groundPlane;
        private readonly Vector3 cellSizeVec;

        [Inject]
        public GridHoverPresenter(
            CityGrid grid,
            GridWorldConverter converter,
            PlacementInputAdapter input,
            GridHighlightView highlight)
        {
            this.grid = grid;
            this.converter = converter;
            this.input = input;
            this.highlight = highlight;

            groundPlane = new Plane(Vector3.up, Vector3.zero);
            cellSizeVec = new Vector3(grid.CellSize, 0f, grid.CellSize);
        }

        public void PostStart() { }

        public void Tick()
        {
            if (!input.TryGetMouseRay(out var ray)) return;
            if (!groundPlane.Raycast(ray, out var dist)) return;

            var hit = ray.GetPoint(dist);

            var cell = converter.ToCell(hit);

            var worldCenter = converter.ToWorldCenter(cell);
            var allowed = grid.IsInside(cell) && !grid.IsOccupied(cell);

            highlight.SetWorldTransform(worldCenter, cellSizeVec, 0.01f);
            highlight.SetAllowed(allowed);
        }
    }
}