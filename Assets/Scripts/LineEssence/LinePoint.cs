using UnityEngine;

namespace LineEssence
{
    public class LinePoint
    {
        private Vector2 _position;
        private float _length;
        
        public Vector2 Position => _position;

        public float Length => _length;

        public LinePoint(Vector2 position, float length)
        {
            _position = position;
            _length = length;
        }
    }
}