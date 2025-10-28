using TMPro;
using UnityEngine;

namespace Presentation.HUD
{
    public sealed class WalletHUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text balanceText;

        public void SetValue(int gold)
        {
            if (balanceText != null)
                balanceText.text = $"gold: {gold}";
        }
    }
}