using UnityEngine;

namespace Presentation.Gameplay.Views
{
    /// <summary>Отображение уровня здания и связь с инстансом (Id).</summary>
    [DisallowMultipleComponent]
    public sealed class BuildingView : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Renderer targetRenderer;

        [Header("Visuals")]
        [SerializeField] private Gradient levelTint =
            new Gradient {
                colorKeys = new[] {
                    new GradientColorKey(new Color(0.85f, 0.85f, 0.85f), 0f),
                    new GradientColorKey(new Color(0.2f, 0.8f, 0.2f),    1f),
                }
            };

        [SerializeField] private float baseScaleY = 1f;
        [SerializeField] private float scalePerLevel = 0.1f;

        public int InstanceId { get; private set; } = -1;
        public int CurrentLevel { get; private set; } = 1;

        Material _mat;

        private void Awake()
        {
            if (targetRenderer != null)
                _mat = targetRenderer.material;
        }

        private void OnDestroy()
        {
            if (_mat != null)
            {
                if (UnityEngine.Application.isPlaying) Destroy(_mat);
                else DestroyImmediate(_mat);
            }
        }

        public void BindInstanceId(int id) => InstanceId = id;

        public void ApplyLevel(int level)
        {
            CurrentLevel = Mathf.Max(1, level);

            if (_mat != null)
            {
                var t = Mathf.InverseLerp(1, 5, CurrentLevel); 
                _mat.color = levelTint.Evaluate(t);
            }

            var s = transform.localScale;
            s.y = baseScaleY + scalePerLevel * (CurrentLevel - 1);
            transform.localScale = s;
        }
    }
}