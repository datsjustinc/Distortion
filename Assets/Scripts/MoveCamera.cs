using UnityEngine;
using Player;

namespace Player
{
    /// <summary>
    /// This class allows camera to move with player.
    /// </summary>
    public class MoveCamera : MonoBehaviour
    {
        // camera object on player to track
        [SerializeField] private Transform cameraPosition;
        
        // getters and setters
        public Transform CameraPosition { get => cameraPosition; set => cameraPosition = value; }

        /// <summary>
        /// This function is called every frame.
        /// </summary>
        private void Update()
        {
            // move camera to player camera position
            transform.position = cameraPosition.position;
        }
    }
}
