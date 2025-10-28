using UnityEngine;

namespace Presentation.Gameplay.Views
{
    /// <summary>
    /// Отрисовка линий сетки как единой меш-сетки (MeshTopology.Lines).
    /// Делать Build(...) один раз на старте/из инсталлеров.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class GridOverlayView : MonoBehaviour
    {
        [SerializeField] private Color lineColor = new Color(1f, 1f, 1f, 0.25f);
        [SerializeField] private float yOffset = 0.015f; 
        [SerializeField] private Material lineMaterial;  

        private Mesh _mesh;

        public void Build(int width, int height, float cellSize, Vector3 origin)
        {
            if (_mesh != null)
            {
                if (UnityEngine.Application.isPlaying) Destroy(_mesh);
                else DestroyImmediate(_mesh);
            }

            _mesh = new Mesh { name = "GridOverlayMesh" };
            GetComponent<MeshFilter>().sharedMesh = _mesh;

            if (lineMaterial == null)
            {
                var shader = Shader.Find("Universal Render Pipeline/Unlit");
                lineMaterial = new Material(shader);
                lineMaterial.enableInstancing = true;
                lineMaterial.renderQueue = 3000; 
                lineMaterial.color = lineColor;
            }
            var mr = GetComponent<MeshRenderer>();
            mr.sharedMaterial = lineMaterial;

            int lines = (width + 1) + (height + 1);
            Vector3[] verts = new Vector3[lines * 2];
            int[] idx = new int[lines * 2];

            int v = 0;
            float y = origin.y + yOffset;

            for (int x = 0; x <= width; x++)
            {
                float wx = origin.x + x * cellSize;
                verts[v] = new Vector3(wx, y, origin.z);
                idx[v] = v;
                v++;
                verts[v] = new Vector3(wx, y, origin.z + height * cellSize);
                idx[v] = v;
                v++;
            }

            for (int z = 0; z <= height; z++)
            {
                float wz = origin.z + z * cellSize;
                verts[v] = new Vector3(origin.x, y, wz);
                idx[v] = v;
                v++;
                verts[v] = new Vector3(origin.x + width * cellSize, y, wz);
                idx[v] = v;
                v++;
            }

            _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            _mesh.SetVertices(verts);
            _mesh.SetIndices(idx, MeshTopology.Lines, 0, true);
            _mesh.RecalculateBounds();
        }

        private void OnValidate()
        {
            if (lineMaterial != null) lineMaterial.color = lineColor;
        }
    }
}