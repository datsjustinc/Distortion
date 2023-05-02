using UnityEngine;

namespace Player
{
    /// <summary>
    /// This class implements the player shooting mechanic.
    /// </summary>
    public class Gun : MonoBehaviour
    {
        [Header("Gun Components")]
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject bulletPoint;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private bool canShoot;
        [SerializeField] private AudioSource audio;
        [SerializeField] private AudioClip bullet;

        /// <summary>
        /// This function is called at start of game.
        /// </summary>
        private void Start()
        {
            // initialize starting bullet speed
            bulletSpeed = 500f;
        }
        
        /// <summary>
        /// This function is called every frame.
        /// </summary>
        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        /// <summary>
        /// This function creates bullet prefab and targeting.
        /// </summary>
        private void Shoot()
        {
            // instantiate bullet prefab
            var bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
            
            audio.clip = this.bullet;
            audio.Play();
            
            // add force to bullet
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Force);
            
            // destroy bullet instance after short delay
            Destroy(bullet, 0.5f);

        }
    }
}
