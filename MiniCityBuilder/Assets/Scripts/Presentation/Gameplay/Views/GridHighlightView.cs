using UnityEngine;

namespace Presentation.Gameplay.Views
{
    /// <summary>Пассивная вьюшка: подсветка клетки под курсором.</summary>
    public sealed class GridHighlightView : MonoBehaviour
    {
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private Color okColor = new(0f, 1f, 0f, 0.35f);
        [SerializeField] private Color badColor = new(1f, 0f, 0f, 0.35f);

        private Material _instanceMat;

        private void Awake()
        {
            if (targetRenderer != null)
            {
                _instanceMat = targetRenderer.material;
                _instanceMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            }
        }

        public void SetWorldTransform(Vector3 worldCenter, Vector3 cellSize, float y = 0.01f)
        {
            transform.position = new Vector3(worldCenter.x, y, worldCenter.z);
            transform.localScale = new Vector3(cellSize.x, 1f, cellSize.z);
        }

        public void SetAllowed(bool allowed)
        {
            if (_instanceMat == null) return;
            _instanceMat.color = allowed ? okColor : badColor;
        }
        
        public void SetTint(Color c)
        {
            if (_instanceMat == null) return;
            _instanceMat.color = c;
        }

        private void OnDestroy()
        {
            if (_instanceMat != null)
            {
                if (UnityEngine.Application.isPlaying) Destroy(_instanceMat);
                else DestroyImmediate(_instanceMat);
            }
        }
    }
}