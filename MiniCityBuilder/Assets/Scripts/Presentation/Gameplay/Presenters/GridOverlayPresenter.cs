using Domain.Gameplay.Models;
using Infrastructure.Grid;
using Presentation.Gameplay.Views;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    /// <summary>
    /// Собирает видимую сетку из параметров CityGrid/конвертера.
    /// </summary>
    public sealed class GridOverlayPresenter : IStartable
    {
        readonly CityGrid grid;
        readonly GridWorldConverter converter;
        readonly GridOverlayView view;

        [Inject]
        public GridOverlayPresenter(CityGrid grid, GridWorldConverter converter, GridOverlayView view)
        {
            this.grid = grid;
            this.converter = converter;
            this.view = view;
        }

        public void Start()
        {
            view.Build(grid.Width, grid.Height, grid.CellSize, converter.Origin); 
        }
    }
}