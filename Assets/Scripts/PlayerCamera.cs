using UnityEngine;
using Player;

namespace Player
{
    /// <summary>
    /// This class implements player camera.
    /// </summary>
    public class PlayerCamera : MonoBehaviour
    {
        // mouse sensitivity
        [Header("Mouse Sensitivity")]
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;

        [Space]
        
        // player orientation stores facing direction
        public Transform orientation;
        
        // rotation of camera
        [SerializeField] private float xRotation;
        [SerializeField] private float yRotation;
        
        // getters and setters
        public float SensX { get => sensX; set => sensX = value; }
        public float SensY { get => sensY; set => sensY = value; }
        public Transform Orientation { get => orientation; set => orientation = value; }
        public float XRotation { get => xRotation; set => xRotation = value; }
        public float YRotation { get => yRotation; set => yRotation = value; }

        /// <summary>
        /// This function is called at start of game.
        /// </summary>
        private void Start()
        {
            // makes sure cursor is in center of screen and invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /// <summary>
        /// This function is called every frame.
        /// </summary>
        private void Update()
        {
            // get mouse inputs
            var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            
            yRotation += mouseX;
            xRotation -= mouseY;

            // clamp x axis rotation
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            // rotate camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}

