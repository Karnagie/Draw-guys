using System;
using DG.Tweening;
using Effects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GuyEssence
{
    [RequireComponent(typeof(Collider))]
    public class Guy : MonoBehaviour
    {
        [SerializeField] private string _runAnimation = "run";
        [SerializeField] private GuyRagdoll _ragdoll;
        [SerializeField] private Animator _animator;
        [SerializeField] private GuyFX _fx;
        
        public event Action<Guy> OnGuyCollided;
        public event Action<Guy> OnDying;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Guy>(out var guy))
            {
                OnGuyCollided?.Invoke(guy);
            }
        }

        public async void Die()
        {
            OnDying?.Invoke(this);
            _animator.enabled = false;
            transform.SetParent(null);
            _ragdoll.Play();
            await _fx.Die();
        }

        public void Go()
        {
            _animator.Play(_runAnimation);
        }
    }
}