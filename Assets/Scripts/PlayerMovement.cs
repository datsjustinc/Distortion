using UnityEngine;

namespace Player
{
    // This class implements player movement physics.
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]

        // player movement speed and facing direction
        [SerializeField] private float moveSpeed;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float drag;

        [Space] [Header("Movement Audio")] 
        [SerializeField] private AudioSource audio;
        [SerializeField] private AudioClip walk;
        [SerializeField] private AudioClip sprint;
        
        [Space]

        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float cooldown;
        [SerializeField] private float multiplier;
        [SerializeField] private bool canJump;
        [SerializeField] private Transform orientation;

        [Space]
        
        [Header("Keys")] 
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

        [Space]
        
        [Header("Ground Check")]

        // check if player is on ground before applying drag
        [SerializeField]
        private float playerHeight;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool grounded;
        
        [Space]
        
        // horizontal and vertical keyboard inputs
        [SerializeField] private float horizontalInput;
        [SerializeField] private float verticalInput;

        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;

        // getters and setters
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Drag { get => drag; set => drag = value; }
        public float JumpForce { get => jumpForce; set => jumpForce = value; }
        public float Cooldown { get => cooldown; set => cooldown = value; }
        public float Multiplier { get => multiplier; set => multiplier = value; }
        public float DragSpeed { get => drag; set => drag = value; }
        public Transform Orientation { get => orientation; set => orientation = value; }
        public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
        public float VerticalInput { get => verticalInput; set => verticalInput = value; }

        /// <summary>
        /// This function is called at start of game.
        /// </summary>
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            canJump = true;
        }

        /// <summary>
        /// This function is called every frame.
        /// </summary>
        private void Update()
        {
            KeyboardInput();
            SpeedControl();
            State();
            
            // ground check shoots raycast from current position
            // down (ray length is a bit more than half of player's height)
            grounded = Physics.Raycast(transform.position, 
                Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

            _rigidbody.drag = grounded ? drag : 0f;
        }

        /// <summary>
        /// This function is called multiple times a frame for more accurate physics calculations.
        /// </summary>
        private void FixedUpdate()
        {
            MovePlayer();
        }

        /// <summary>
        /// This function grabs keyboard inputs
        /// </summary>
        private void KeyboardInput()
        {
            horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
            verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");
            
            // jump trigger condition
            if (Input.GetKey(jumpKey) && canJump && grounded)
            {
                canJump = false;
                Jump();
                
                // prevent infinite jump glitch
                Invoke(nameof(ResetJump), cooldown);
            }
        }

        /// <summary>
        /// This function updates player movement location.
        /// </summary>
        private void MovePlayer()
        {
            // calculate move direction and always walk in direction looking
            _moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (_moveDirection == Vector3.zero)
            {
                audio.Stop();
            }
            
            if (_moveDirection != Vector3.zero && !audio.isPlaying)
            {
                audio.Play();
            }

            // add force to player movement in calculated direction from before if not in air
            if (grounded)
            {
                _rigidbody.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
            }
            else
            {
                _rigidbody.AddForce(_moveDirection.normalized * (moveSpeed * 10f * multiplier), ForceMode.Force);
            }
        }

        /// <summary>
        /// This functions limits player speed manually.
        /// </summary>
        private void SpeedControl()
        {
            var velocity = _rigidbody.velocity;
            var flatVelocity = new Vector3(velocity.x, 0f, velocity.z);
            
            // limit velocity if player goes faster than movement speed
            if (flatVelocity.magnitude > moveSpeed)
            {
                // calculate possible max velocity
                var limitedVelocity = flatVelocity.normalized * moveSpeed;
                
                // and apply max velocity at the speed cap
                _rigidbody.velocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
            }
            
        }

        /// <summary>
        /// This function allows player to jump.
        /// </summary>
        private void Jump()
        {
            // reset y velocity so player always jump from exact same height
            var velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
            
            // add force to player jump and use Impulse mode because only apply force once
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        /// <summary>
        /// This function allows player to reset and jump again.
        /// </summary>
        private void ResetJump()
        {
            canJump = true;
        }

        private void State()
        {
            // if sprint key is pressed
            if (grounded && Input.GetKey(sprintKey))
            {
                audio.clip = sprint;
                moveSpeed = sprintSpeed;
            }
            
            // if not sprinting, then walking
            else if (grounded)
            {
                audio.clip = walk;
                moveSpeed = walkSpeed;
            }

            // if in air
            else
            {
                audio.clip = null;
            }
        }
    }
}

