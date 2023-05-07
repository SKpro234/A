using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius;  //for grounded state
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool isGrounded; 

    private float jumpCount = 0;
    public float maxJumpCount;

    public Rigidbody2D rb; 
    public float moveSpeed;
    public float jumpForce;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        if(isGrounded)
        {
            jumpCount = 0;
        }
        

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        
        Debug.Log(jumpCount);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("boom");
    }

    private void Jump(InputAction.CallbackContext context)
    {

        if(jumpCount < maxJumpCount){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
}
