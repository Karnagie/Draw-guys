using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LineEssence
{
    public class LineDrawer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _image;

        public event Action<Line> OnLineDrew;
        
        private bool _isClicked;
        private Line _line;

        private void Awake()
        {
            _line = new Line();
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() && _isClicked)
            {
                var rect = _image.rectTransform.rect;
                var size = new Vector2( rect.width,  rect.height);
                var pos = GetMousePosition() / size;
                _line.AddPoint(pos);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _line.Clear();
            _isClicked = true; 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnLineDrew?.Invoke(_line);
            _isClicked = false;
        }
        
        private Vector2 GetMousePosition()
        {
            var pos = Mouse.current.position.ReadValue();
            var offset = _image.rectTransform.offsetMin;
            pos.x -= offset.x;
            pos.y -= offset.y;
            return pos;
        }
    }
}