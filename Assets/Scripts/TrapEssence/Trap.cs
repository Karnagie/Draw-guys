using System;
using GuyEssence;
using UnityEngine;

namespace TrapEssence
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Guy>(out var guy))
            {
                guy.Die();
            }
        }
    }
}