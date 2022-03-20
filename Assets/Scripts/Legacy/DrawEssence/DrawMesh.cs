using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Legacy.DrawEssence
{
    public class DrawMesh : MonoBehaviour
    {
        [SerializeField] private float _lineThickness = 10f;
        [SerializeField] private float _minDistance = 0.2f;
        [SerializeField] private Camera _cam;
        [SerializeField] private MeshFilter _filter;
        
        private Mesh _mesh;
        private MainActions _actions;
        private bool _isClicked;
        private Vector3 _lastMousePosition;
        
        private void Awake()
        {
            _actions = new MainActions();
            _actions.Player.MousePressing.started += ctx =>
            {
                CreateMesh(); 
                _isClicked = true; 
            };
            _actions.Player.MousePressing.canceled += ctx => { _isClicked = false; };
            _actions.Enable();
        }

        private void Update()
        {
            Vector3 worldPoint = GetMousePosition();

            if (_isClicked)
            {
                if(Vector3.Distance(worldPoint, _lastMousePosition) < _minDistance) return;

                Vector3[] vertices = new Vector3[_mesh.vertices.Length + 2];
                Vector2[] uv = new Vector2[_mesh.uv.Length + 2];
                int[] triangles = new int[_mesh.triangles.Length + 6];
                
                _mesh.vertices.CopyTo(vertices, 0);
                _mesh.uv.CopyTo(uv, 0);
                _mesh.triangles.CopyTo(triangles, 0);

                int vIndex = vertices.Length - 4;
                int vIndex0 = vIndex + 0;
                int vIndex1 = vIndex + 1;
                int vIndex2 = vIndex + 2; 
                int vIndex3 = vIndex + 3;
                
                Vector3 mouseForwardVector = (worldPoint - _lastMousePosition).normalized;
                Vector3 normal2D = new Vector3(0, 0, -1);
                Vector3 nextVertexUp = worldPoint + Vector3.Cross(mouseForwardVector, normal2D) * _lineThickness;
                Vector3 nextVertexDown = worldPoint + Vector3.Cross(mouseForwardVector, normal2D * -1f) * _lineThickness;

                vertices[vIndex2] = nextVertexUp;
                vertices[vIndex3] = nextVertexDown;
                
                uv[vIndex2] = Vector2.zero;
                uv[vIndex3] = Vector2.zero;

                int tIndex = triangles.Length - 6;

                triangles[tIndex + 0] = vIndex0;
                triangles[tIndex + 1] = vIndex2;
                triangles[tIndex + 2] = vIndex1;
                
                triangles[tIndex + 3] = vIndex1;
                triangles[tIndex + 4] = vIndex2;
                triangles[tIndex + 5] = vIndex3;

                _mesh.vertices = vertices;
                _mesh.uv = uv;
                _mesh.triangles = triangles;
                
                _lastMousePosition = worldPoint;
            }
        }

        private void CreateMesh()
        {
            Vector3 worldPoint = GetMousePosition();
            
            _mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            vertices[0] = worldPoint;
            vertices[1] = worldPoint;
            vertices[2] = worldPoint;
            vertices[3] = worldPoint;
            
            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;
            
            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.MarkDynamic();
            
            _filter.mesh = _mesh;
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = _cam.farClipPlane * .01f;
            return _cam.ScreenToWorldPoint(mousePos);
        }
    }
}