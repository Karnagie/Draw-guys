using UnityEngine;

namespace Effects
{
    public class ParticleGroup : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particles;

        public void Play()
        {
            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
    }
}