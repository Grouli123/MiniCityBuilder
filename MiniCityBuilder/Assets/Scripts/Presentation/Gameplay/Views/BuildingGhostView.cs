using UnityEngine;

namespace Presentation.Gameplay.Views
{
    /// <summary>Пассивная view «призрака» здания — показывает выбранный тип там, где будет размещение.</summary>
    public sealed class BuildingGhostView : MonoBehaviour
    {
        [Header("Targets on the Ghost object")]
        [SerializeField] private MeshFilter targetFilter;    
        [SerializeField] private Renderer targetRenderer;     
        [SerializeField] private float y = 0.02f;

        [Header("Colors")]
        [SerializeField] private Color okColor  = new(0f, 1f, 0f, 0.35f);
        [SerializeField] private Color badColor = new(1f, 0f, 0f, 0.35f);

        Material _instMat;

        void Awake()
        {
            if (targetRenderer != null)
            {
                _instMat = targetRenderer.material;
                _instMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                var c = _instMat.color; c.a = 0.35f; _instMat.color = c;
            }
        }

        void OnDestroy()
        {
            if (_instMat != null)
            {
                if (Application.isPlaying) Destroy(_instMat);
                else DestroyImmediate(_instMat);
            }
        }

        /// <summary>Подменить меш/материал по образцу из префаба типа здания.</summary>
        public void SetPrefab(GameObject prefab)
        {
            if (prefab == null) return;

            var srcFilter = prefab.GetComponentInChildren<MeshFilter>(true);
            var srcRenderer = prefab.GetComponentInChildren<MeshRenderer>(true);

            if (srcFilter != null && targetFilter != null)
                targetFilter.sharedMesh = srcFilter.sharedMesh;

            if (srcRenderer != null && _instMat != null)
            {
                _instMat.CopyPropertiesFromMaterial(srcRenderer.sharedMaterial);
                _instMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                var c = _instMat.color; c.a = 0.35f; _instMat.color = c;
            }
        }

        public void SetWorldCenter(Vector3 worldCenter)
        {
            transform.position = new Vector3(worldCenter.x, y, worldCenter.z);
        }

        public void SetAllowed(bool allowed)
        {
            if (_instMat == null) return;
            _instMat.color = allowed ? okColor : badColor;
        }
    }
}