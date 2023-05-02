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
        [SerializeField] private float damage = 10f;
        
        private void OnCollisionEnter(Collision collision)
        {
            impact.Play();

            // check if collision with enemy
            if (collision.gameObject.CompareTag("Enemy")) ;
            {
                // change bullets parent to enemy object
                transform.parent = collision.transform;
                
                // enemy takes damage
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
