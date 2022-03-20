using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GuyEssence
{
    public class GuyRagdoll : MonoBehaviour
    {
        [SerializeField] private float _forceMultiplier = 300;
        [SerializeField] private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
        }

        public void Play()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
                Vector3 randomForce = Vector3.up;
                randomForce.x += Random.Range(0.1f, 0.3f);
                randomForce.z += Random.Range(0.1f, 0.3f);
                randomForce *= Random.Range(1, 3);
                rigidbody.AddForce(randomForce*_forceMultiplier);
            }
        }
    }
}