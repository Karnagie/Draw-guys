using System.Threading.Tasks;
using DG.Tweening;
using Effects;
using UnityEngine;

namespace GuyEssence
{
    public class GuyFX : MonoBehaviour
    {
        [SerializeField] private ParticleGroup _deathEffect;
        
        public async Task Die()
        {
            ParticleGroup effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            effect.Play();
            transform.DOScale(0, 1).OnComplete((() =>
            {
                transform.DOKill();
                Destroy(gameObject);
            }));
        }
    }
}