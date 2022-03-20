using System;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _direction;

        private void Awake()
        {
            transform.DORotate(_direction, _speed).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}