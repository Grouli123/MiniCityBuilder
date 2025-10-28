using UnityEngine;
using UnityEngine.InputSystem;

namespace Presentation.CameraRig
{
    /// <summary>
    /// Простое RTS-управление: WASD/стрелки — панорамирование,
    /// колесо — зум, Q/E — поворот, ПКМ — drag, удерживая Shift — ускорение.
    /// Вешаешь на Main Camera.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CameraOrbitController : MonoBehaviour
    {
        [Header("Target / Rig")]
        [SerializeField] private Transform pivot;  
        [SerializeField] private float distance = 12f;
        [SerializeField] private float minDistance = 6f;
        [SerializeField] private float maxDistance = 40f;

        [Header("Speeds")]
        [SerializeField] private float panSpeed   = 10f; 
        [SerializeField] private float rotateSpeed= 90f; 
        [SerializeField] private float zoomSpeed  = 8f; 

        [Header("Limits")]
        [SerializeField] private Vector2 yawLimits   = new Vector2(-999f, 999f); 
        [SerializeField] private Vector2 pitchLimits = new Vector2(20f, 80f);   

        private float yaw = 45f;
        private float pitch = 60f;

        private void Reset()
        {
            if (pivot == null)
            {
                var go = new GameObject("CameraPivot");
                go.transform.position = Vector3.zero;
                pivot = go.transform;
            }
        }

        private void LateUpdate()
        {
            if (pivot == null) return;

            var kb = Keyboard.current;
            var ms = Mouse.current;

            Vector3 move = Vector3.zero;
            if (kb != null)
            {
                if (kb.wKey.isPressed || kb.upArrowKey.isPressed)    move += Vector3.forward;
                if (kb.sKey.isPressed || kb.downArrowKey.isPressed)  move += Vector3.back;
                if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  move += Vector3.left;
                if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) move += Vector3.right;

                if (move.sqrMagnitude > 0f)
                {
                    float spd = panSpeed * (kb.shiftKey.isPressed ? 2f : 1f);
                    var dir = Quaternion.Euler(0f, yaw, 0f) * move.normalized;
                    pivot.position += dir * spd * Time.deltaTime;
                }

                if (kb.qKey.isPressed) yaw -= rotateSpeed * Time.deltaTime;
                if (kb.eKey.isPressed) yaw += rotateSpeed * Time.deltaTime;
            }

            if (ms != null && ms.rightButton.isPressed)
            {
                Vector2 delta = ms.delta.ReadValue();
                yaw   += delta.x * 0.1f;
                pitch -= delta.y * 0.1f;
            }

            yaw   = Mathf.Clamp(yaw,   yawLimits.x,   yawLimits.y);
            pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

            if (ms != null)
            {
                float scroll = ms.scroll.ReadValue().y; // + вверх
                if (Mathf.Abs(scroll) > 0.01f)
                {
                    distance *= Mathf.Pow(1f - zoomSpeed * 0.01f, scroll * Time.deltaTime * 60f);
                    distance = Mathf.Clamp(distance, minDistance, maxDistance);
                }
            }

            var rot = Quaternion.Euler(pitch, yaw, 0f);
            var pos = pivot.position - rot * Vector3.forward * distance;
            transform.SetPositionAndRotation(pos, rot);
        }
    }
}