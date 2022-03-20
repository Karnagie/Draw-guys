using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LineEssence
{
    public class DrawManager : MonoBehaviour
    {
        [SerializeField] private ComputeShader _drawComputeShader;
        [SerializeField] private Color _backgroundColour;
        [SerializeField] private Color _brushColour;
        [SerializeField] private float _brushSize = 10f;
        [SerializeField] private Image _image;
        [SerializeField]
        [Range(0.01f, 1)] private float _strokeSmoothingInterval = 0.1f;
    
        RenderTexture _canvasRenderTexture;
        private Vector4 _previousMousePosition;
        private MainActions _actions;
        private bool _isClicked;

        void Start()
        {
            _actions = new MainActions();
            _actions.Player.MousePressing.started += ctx =>
            {
                _isClicked = true; 
            };
            _actions.Player.MousePressing.canceled += ctx => { _isClicked = false; };
            _actions.Enable();
        
            var sizeDelta = _image.rectTransform.rect;
            _canvasRenderTexture = new RenderTexture((int)sizeDelta.width, (int)sizeDelta.height, 24);
            _canvasRenderTexture.filterMode = FilterMode.Point;
            _canvasRenderTexture.enableRandomWrite = true;
            _canvasRenderTexture.Create();

            int initBackgroundKernel = _drawComputeShader.FindKernel("InitBackground");
            _drawComputeShader.SetVector("_BackgroundColour", _backgroundColour);
            _drawComputeShader.SetTexture(initBackgroundKernel, "_Canvas", _canvasRenderTexture);
            _drawComputeShader.SetFloat("_CanvasWidth", _canvasRenderTexture.width);
            _drawComputeShader.SetFloat("_CanvasHeight", _canvasRenderTexture.height);
            _drawComputeShader.GetKernelThreadGroupSizes(initBackgroundKernel,
                out uint xGroupSize, out uint yGroupSize, out _);
            _drawComputeShader.Dispatch(initBackgroundKernel,
                Mathf.CeilToInt(_canvasRenderTexture.width / (float) xGroupSize),
                Mathf.CeilToInt(_canvasRenderTexture.height / (float) yGroupSize),
                1);
            _drawComputeShader.Dispatch(initBackgroundKernel,
                Mathf.CeilToInt(_canvasRenderTexture.width / (float) xGroupSize),
                Mathf.CeilToInt(_canvasRenderTexture.height / (float) yGroupSize),
                1);
        
            _image.material.mainTexture = _canvasRenderTexture;
            _previousMousePosition = GetMousePosition();
        }

        void Update()
        {
            int updateKernel = _drawComputeShader.FindKernel("Update");
            _drawComputeShader.SetVector("_PreviousMousePosition", _previousMousePosition);
            _drawComputeShader.SetVector("_MousePosition", GetMousePosition());
            _drawComputeShader.SetBool("_MouseDown", _isClicked);
            _drawComputeShader.SetFloat("_BrushSize", _brushSize);
            _drawComputeShader.SetVector("_BrushColour", _brushColour);
            _drawComputeShader.SetFloat("_StrokeSmoothingInterval", _strokeSmoothingInterval);
            _drawComputeShader.SetTexture(updateKernel, "_Canvas", _canvasRenderTexture);
            _drawComputeShader.SetFloat("_CanvasWidth", _canvasRenderTexture.width);
            _drawComputeShader.SetFloat("_CanvasHeight", _canvasRenderTexture.height);

            _drawComputeShader.GetKernelThreadGroupSizes(updateKernel,
                out uint xGroupSize, out uint yGroupSize, out _);
            _drawComputeShader.Dispatch(updateKernel,
                Mathf.CeilToInt(_canvasRenderTexture.width / (float) xGroupSize),
                Mathf.CeilToInt(_canvasRenderTexture.height / (float) yGroupSize),
                1);
            _previousMousePosition = GetMousePosition();
        }
    
        private Vector4 GetMousePosition()
        {
            var pos = Mouse.current.position.ReadValue();
            var offset = _image.rectTransform.offsetMin;
            pos.x -= offset.x;
            pos.y -= offset.y;
            return pos;
        }
    }
}