using UnityEngine;
using AI;

namespace AI
{
    /// <summary>
    /// This function defines the enemy class.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Components")]
        [SerializeField] private float health = 1000f;
        [SerializeField] private Animator anim;

        /// <summary>
        /// This function allows enemy to take damage.
        /// </summary>
        /// <param name="damage">value to deduct form enemy health</param>
        public void TakeDamage(float damage)
        {

            // no health, enemy is dead
            if (health <= 0)
            {
                anim.SetTrigger("Death");
                
                // prevents player from triggering enemy death animation repeatedly
                gameObject.GetComponent<Collider>().enabled = false;
            }

            // otherwise enemy just takes damage 
            else
            {
                anim.SetTrigger("Damage");
            }
        }
    }
}
