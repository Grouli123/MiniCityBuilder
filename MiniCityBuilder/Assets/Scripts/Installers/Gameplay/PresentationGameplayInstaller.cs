using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers.Gameplay
{
    /// <summary>Инсталлер презентационного слоя геймплея.</summary>
    public sealed class PresentationGameplayInstaller : LifetimeScope
    {
        [SerializeField] private GridHighlightView gridHighlightView;
        [SerializeField] private Infrastructure.Input.PlacementInputAdapter inputAdapter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(gridHighlightView);
            builder.RegisterInstance(inputAdapter);

            builder.Register<GridHoverPresenter>(Lifetime.Singleton);
        }
    }
}