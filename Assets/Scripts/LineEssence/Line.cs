using System;
using System.Collections.Generic;
using UnityEngine;

namespace LineEssence
{
    public class Line
    {
        private List<LinePoint> _points;
        private float _length;

        public Line()
        {
            _points = new List<LinePoint>();
            _length = 0;
        }

        public Vector3[] Separate(int count)
        {
            Vector3[] offsets = new Vector3[count];
            count -= 1;
            int index = 0;
            float distanceBetween = _length / count;
            
            float nextDistance = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                if (offsets.Length == index) break;
                if (Math.Abs(nextDistance - _points[i].Length) <= 0.01f)
                {
                    offsets[index] = _points[i].Position;
                    nextDistance += distanceBetween;
                    index++;
                    i--;
                    continue;
                }
                if (nextDistance < _points[i].Length)
                {
                    LinePoint previousPoint = _points[i - 1];
                    Vector2 normalized = (_points[i].Position-previousPoint.Position).normalized;
                    float remainedLength = nextDistance - previousPoint.Length;
                    Vector2 remainedVector =  previousPoint.Position + (normalized * remainedLength);
                    offsets[index] = remainedVector;
                    nextDistance += distanceBetween;
                    index++;
                    i--;
                }
            }

            return offsets;
        }

        public void AddPoint(Vector2 position)
        {
            if (_points.Count != 0)
            {
                _length += (position - _points[_points.Count - 1].Position).magnitude;
            }

            _points.Add(new LinePoint(position, _length));
        }

        public void Clear()
        {
            _points = new List<LinePoint>();
            _length = 0;
        }
    }
}