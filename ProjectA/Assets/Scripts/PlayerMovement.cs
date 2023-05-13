using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool isGrounded; 

    private float jumpCount = 0;
    public float maxJumpCount;

    private bool isFacingRight = true;
    public Rigidbody2D rb; 
    public float moveSpeed;
    public float jumpForce;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction dash;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

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

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
        dash.Disable();
    }

    void Update()
    {
        if(isDashing){
            return;
        }
        moveDirection = move.ReadValue<Vector2>();
        isGrounded = IsGrounded();
        if(isGrounded)
        {
            jumpCount = 0;
        }          
        Flip();
    
    }

    private void FixedUpdate()
    {
        if(isDashing){
            return;
        }
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        
        Debug.Log(jumpCount);
    }

    public bool IsGrounded(){
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("boom");
    }

    private void Jump(InputAction.CallbackContext context)
    {

        if(isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if(jumpCount < maxJumpCount){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
    private void Dash(InputAction.CallbackContext context)
    {
        if(canDash){
            StartCoroutine(Dash());
        }
    }

    private void Flip(){
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
