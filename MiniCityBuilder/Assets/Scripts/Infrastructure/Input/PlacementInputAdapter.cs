using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Input
{
    /// <summary>Адаптер ввода для размещения зданий: луч из камеры, клики, хоткеи.</summary>
    public sealed class PlacementInputAdapter : MonoBehaviour
    {
        [SerializeField] private Camera sceneCamera;

        public bool LeftClickDown { get; private set; }
        public bool RightClickDown { get; private set; }     
        public bool UpgradePressed { get; private set; }     
        public Vector2 MousePosition { get; private set; }
        public int SelectedTypeIndex { get; private set; } = 0;

        private void Reset()
        {
            if (sceneCamera == null) sceneCamera = Camera.main;
        }

        private void Update()
        {
            var mouse = Mouse.current;
            var kb    = Keyboard.current;
            if (mouse == null || kb == null) return;

            MousePosition  = mouse.position.value;
            LeftClickDown  = mouse.leftButton.wasPressedThisFrame;
            RightClickDown = mouse.rightButton.wasPressedThisFrame;    
            UpgradePressed = kb.uKey.wasPressedThisFrame;               

            if (kb.digit1Key.wasPressedThisFrame) SelectedTypeIndex = 0;
            if (kb.digit2Key.wasPressedThisFrame) SelectedTypeIndex = 1;
            if (kb.digit3Key.wasPressedThisFrame) SelectedTypeIndex = 2;
        }

        public bool TryGetMouseRay(out Ray ray)
        {
            if (sceneCamera == null) { ray = default; return false; }
            ray = sceneCamera.ScreenPointToRay(MousePosition);
            return true;
        }
    }
}