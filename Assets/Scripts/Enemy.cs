using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace AI
{
    /// <summary>
    /// This function defines the enemy class.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Components")] 
        [SerializeField] private float maxHealth;
        [SerializeField] private float health;
        [SerializeField] private Animator anim;
        [SerializeField] private Image healthbar;
        [SerializeField] private NavMeshAgent agent;
        
        [Space]
        
        [Header("Enemy Sounds")]
        [SerializeField] private AudioSource audio;
        [SerializeField] private AudioClip death;
        [SerializeField] private AudioClip damage;
        [SerializeField] private AudioClip attack;

        [Space] 
        
        [Header("Enemy Materials")] 
        [SerializeField] private Material material;

        // getters and setters
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float Health { get => health; set => health = value; }
        public NavMeshAgent Agent { get => agent; set => agent = value; }
        
        /// <summary>
        /// This function is called at start of game.
        /// </summary>
        private void Start()
        {
            health = maxHealth;
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        
        /// <summary>
        /// This function is called every frame.
        /// </summary>
        private void Update()
        {
            healthbar.fillAmount = health * (1f/maxHealth);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackState"))
            {
                audio.clip = attack;
                audio.Play();
            }
        }
        
        /// <summary>
        /// This function allows enemy to take damage.
        /// </summary>
        /// <param name="damage">value to deduct form enemy health</param>
        public void TakeDamage(float damage)
        {
            health -= damage;
            
            // no health, enemy is dead
            if (health <= 0)
            {
                if (!audio.isPlaying)
                {
                    audio.clip = death;
                    audio.Play();
                }
                
                anim.SetTrigger("Death");
                
                // prevents player from triggering enemy death animation repeatedly
                gameObject.GetComponent<Collider>().enabled = false;
                Invoke(nameof(Destroy), 3f);
            }

            // otherwise enemy just takes damage 
            else
            {
                if (!audio.isPlaying)
                {
                    audio.clip = this.damage;
                    audio.Play();
                }
                
                anim.SetTrigger("Damage");
            }
        }

        /// <summary>
        /// This function destroys the current game object.
        /// </summary>
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
