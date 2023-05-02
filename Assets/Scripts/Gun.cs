using System;
using System.Collections;
using UnityEngine;
using Player;

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
        
        [Space]
        
        [Header("Gun Animations")]
        [SerializeField] private Camera playerCam;
        [SerializeField] private Animator anim;
        [SerializeField] private float zoomView;
        [SerializeField] private float defaultView;
        [SerializeField] private float zoomSpeed;
        

        /// <summary>
        /// This function is called at start of game.
        /// </summary>
        private void Start()
        {
            // initialize starting bullet speed
            bulletSpeed = 500f;
            playerCam = Camera.main;
        }
        
        /// <summary>
        /// This function is called every frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Zoom();
         
            }

            if (!Input.GetMouseButton(1))
            {
                UnZoom();
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

        /// <summary>
        /// This function allows player to zoom in gun.
        /// </summary>
        private void Zoom()
        {
            anim.SetBool("Zoom", true);
            StartCoroutine(LerpView(1f, zoomView));

        }

        /// <summary>
        /// This function resets player zoom in gun.
        /// </summary>
        private void UnZoom()
        {
            anim.SetBool("Zoom", false);
            StartCoroutine(LerpView(0.3f, defaultView));
        }

        private IEnumerator LerpView(float duration, float targetView)
        {
            float timeElapsed = 0.0f;
            
            // time duration loop
            while (timeElapsed < duration)
            {

                var t = timeElapsed / duration;
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetView, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
