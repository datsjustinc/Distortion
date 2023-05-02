using UnityEngine;
using Player;

namespace Player
{
    /// <summary>
    /// This class implements the bullet particle effects.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem impact;
        
        private void OnCollisionEnter(Collision collision)
        {
            impact.Play();
        }
    }
}
