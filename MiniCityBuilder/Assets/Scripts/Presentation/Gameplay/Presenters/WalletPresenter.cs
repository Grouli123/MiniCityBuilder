using Application.Services;
using Presentation.HUD;             
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public sealed class WalletPresenter : IStartable, System.IDisposable
    {
        readonly IWalletService wallet;
        readonly WalletHUDView view;

        [Inject]
        public WalletPresenter(IWalletService wallet, WalletHUDView view)
        {
            this.wallet = wallet;
            this.view = view;
        }

        public void Start()
        {
            wallet.OnBalanceChanged += view.SetValue;
            view.SetValue(wallet.Balance);
        }

        public void Dispose()
        {
            wallet.OnBalanceChanged -= view.SetValue;
        }
    }
}