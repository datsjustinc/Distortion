using AI;
using UnityEngine;
using Player;

namespace Player
{
    /// <summary>
    /// This class implements the bullet particle and collisions.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem impact;
        [SerializeField] private float damage = 1f;
        
        /// <summary>
        /// This function checks for object collision.
        /// </summary>
        /// <param name="collision">the object collided with</param>
        private void OnCollisionEnter(Collision collision)
        {
            impact.Play();

            // check if collision with enemy
            if (collision.gameObject.CompareTag("Enemy"))
            {
                transform.parent = collision.transform;
                
                // enemy takes damage
                collision.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
