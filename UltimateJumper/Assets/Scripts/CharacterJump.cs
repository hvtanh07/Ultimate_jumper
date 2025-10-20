using UnityEngine;

/// <summary>
/// Controls the player's jump using Rigidbody2D for physics. This script handles variable jump height
/// based on how long the jump button is held by adjusting the gravity scale. It also includes a double jump.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [Tooltip("The initial upward velocity when the jump starts.")]
    [SerializeField] private float initialJumpVelocity = 15f;
    [Tooltip("The maximum time the player can hold the jump button to increase height.")]
    [SerializeField] private float maxJumpTime = 0.35f;
    [Tooltip("The base gravity scale for the Rigidbody2D.")]
    [SerializeField] private float baseGravityScale = 4f;
    [Tooltip("A multiplier applied to gravity when the jump button is released or the character is falling.")]
    [SerializeField] private float fallMultiplier = 1.5f;
    [Tooltip("A multiplier to reduce gravity while the jump button is being held down, allowing for a higher jump.")]
    [SerializeField] private float jumpHoldGravityMultiplier = 0.5f;

    [Header("Double Jump Settings")]
    [Tooltip("The number of additional jumps the player can make while in the air.")]
    [SerializeField] private int airJumpsValue = 1;


    [Header("Ground Check")]
    [Tooltip("The transform representing the position where the ground check is performed (usually at the character's feet).")]
    [SerializeField] private Transform groundCheck;
    [Tooltip("The size of the box used to detect the ground.")]
    [SerializeField] private Vector2 groundCheckBoxSize = new Vector2(0.5f, 0.1f);
    [Tooltip("The radius of the circle used to detect the ground.")]


    [SerializeField] private LayerMask groundLayer;

    // Components and private state variables
    private Rigidbody2D rb;
    private bool isJumping;
    private float jumpTimeCounter;
    private bool isGrounded;
    private int airJumpsLeft;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Perform the ground check and handle jump input in every frame.
        CheckIfGrounded();
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        // Handle physics-related logic like gravity in FixedUpdate.
        HandleGravity();
    }

    /// <summary>
    /// Checks if the character is currently on the ground by casting a small circle at the feet.
    /// </summary>
    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckBoxSize, 0f, groundLayer);

        // If the character is on the ground and not moving up, they are not jumping.
        if (isGrounded && rb.linearVelocity.y <= 0.01f)
        {
            isJumping = false;
            airJumpsLeft = airJumpsValue; // Reset air jumps on landing
        }
    }

    /// <summary>
    /// Manages the character's jump based on player input (mouse click or touch).
    /// </summary>
    private void HandleJumpInput()
    {
        // When the jump button is pressed and the character is grounded:
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, initialJumpVelocity);
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
        }
        // Handle air jump
        else if (Input.GetMouseButtonDown(0) && !isGrounded && airJumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, initialJumpVelocity);
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            airJumpsLeft--; // Consume an air jump
        }


        // While the jump button is held and the character is jumping:
        if (Input.GetMouseButton(0) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                // The actual height increase is now handled by reducing gravity in HandleGravity().
                // We just need to count down the time here.
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                // Max jump time has been reached.
                isJumping = false;
            }
        }

        // When the jump button is released, stop the "jump hold" phase.
        if (Input.GetMouseButtonUp(0))
        {
            isJumping = false;
        }
    }

    /// <summary>
    /// Adjusts the Rigidbody2D's gravity scale to create a variable jump height and a snappier feel.
    /// </summary>
    private void HandleGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            // Increase gravity when falling for a less "floaty" feel.
            rb.gravityScale = baseGravityScale * fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && isJumping && Input.GetMouseButton(0))
        {
            // Reduce gravity while the jump button is being held to achieve a higher jump.
            rb.gravityScale = baseGravityScale * jumpHoldGravityMultiplier;
        }
        else
        {
            // Use the base gravity scale in all other situations (e.g., normal upward arc).
            rb.gravityScale = baseGravityScale;
        }
    }

    /// <summary>
    /// Public method to reset the number of available air jumps.
    /// This can be called from other scripts (e.g., when collecting a power-up).
    /// </summary>
    public void ResetAirJumps()
    {
        airJumpsLeft = airJumpsValue;
    }

    /// <summary>
    /// Draws a gizmo in the editor to visualize the ground check radius.
    /// This is helpful for debugging and setting up the ground check position and radius.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);
    }
}

