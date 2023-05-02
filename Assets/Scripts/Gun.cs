using System;
using System.Collections;
using UnityEngine;
using Player;
using UnityEngine.UI;

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
        [SerializeField] private int bullets;
        [SerializeField] private AudioSource audio;
        [SerializeField] private AudioClip bullet;
        [SerializeField] private AudioClip emptyAmmo;
        [SerializeField] private AudioClip gunReload;
        [SerializeField] private bool reloadFinished;
        [SerializeField] private Image crosshair;
        
        [Space]
        
        [Header("Gun Animations")]
        [SerializeField] private Camera playerCam;
        [SerializeField] private Animator anim;
        [SerializeField] private float zoomView;
        [SerializeField] private float defaultView;

        [Space]
        
        [Header("Gun Ammo")]
        [SerializeField] private Image gunAmmo;
        

        /// <summary>
        /// This function is called at start of game.
        /// </summary>
        private void Start()
        {
            // initialize starting bullet speed
            bulletSpeed = 500f;
            playerCam = Camera.main;
            crosshair.enabled = false;
            bullets = 9;
            reloadFinished = true;
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

            if (Input.GetMouseButtonDown(1) && reloadFinished)
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
            // if no bullets left in gun
            if (bullets <= 0 && reloadFinished)
            {
                audio.clip = emptyAmmo;
                audio.Play();
                reloadFinished = false;
                UnZoom();
                StartCoroutine(Reload());
                return;
            }

            // all ammo needs to be reload before being able to target (buffer time)
            if (!reloadFinished)
            {
                return;
            }
            
            // instantiate bullet prefab
            var bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);

            audio.clip = this.bullet;
            audio.Play();

            // add force to bullet
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Force);
            bullets--;
            gunAmmo.fillAmount -= (1.0f / 9.0f);

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
            crosshair.enabled = true;

        }

        /// <summary>
        /// This function resets player zoom in gun.
        /// </summary>
        private void UnZoom()
        {
            anim.SetBool("Zoom", false);
            crosshair.enabled = false;
            StartCoroutine(LerpView(0.3f, defaultView));
        }

        /// <summary>
        /// This function reloads player gun.
        /// </summary>
        private IEnumerator Reload()
        {
            float timeElapsed = 0.0f;
            Debug.Log("Setting bool");
            anim.SetBool("Reload", true);
            audio.clip = gunReload;
            audio.Play();
            
            // time duration loop
            while (timeElapsed < 2f)
            {
                var t = timeElapsed / 2f;
                gunAmmo.fillAmount = Mathf.Lerp(gunAmmo.fillAmount, 1f, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            reloadFinished = true;
            anim.SetBool("Reload", false);
            bullets = 9;
        }

        /// <summary>
        /// This function transitions camera field of view on gun.
        /// </summary>
        /// <param name="duration">how long the transition lasts</param>
        /// <param name="targetView">target field of view value to lerp to</param>
        /// <returns></returns>
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
