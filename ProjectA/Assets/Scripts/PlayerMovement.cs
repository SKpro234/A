using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;

    public bool isGrounded; 

    private float jumpCount = 0;
    public float maxJumpCount;

    public bool isFacingRight = true;
    public Rigidbody2D rb; 
    public float moveSpeed;
    public float jumpForce;
    private bool isJumping;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;

    private InputAction move;
    private InputAction jump;
    private InputAction dash;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.5f;
    private float jumpBufferCounter;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();


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
        jump.Disable();
        dash.Disable();
    }

    void Update()
    {
        if(isDashing)
        {
            return;
        }
        isGrounded = IsGrounded();
        moveDirection = move.ReadValue<Vector2>();
        if (move.ReadValue<Vector2>().x != 0f)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
        if(rb.velocity.y < 0 && !isGrounded)
        {
            anim.SetBool("isjumping", false);
            anim.SetBool("isfalling", true);
        }
        else if(isJumping == true)
        {
            anim.SetBool("isjumping", true);
            anim.SetBool("isfalling", false);
        }
        if(isGrounded)
        {
            anim.SetBool("isjumping", false);
            anim.SetBool("isfalling", false);
        }
        if(isGrounded && jumpCount != 0) //grounded reset state
        {
            jumpCount = 0;
            isJumping = false;
        }          
        if(isGrounded) //coyote time
        {
            coyoteTimeCounter = coyoteTime;
        }          
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(jumpBufferCounter > 0f && isGrounded)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        jumpBufferCounter -= Time.deltaTime; //jump buffer
        
        Flip();
        
        if(jump.ReadValue<float>() == 0 && isJumping == true && rb.velocity.y > 0)   // 1 space is held down, 0 not held down
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
            coyoteTimeCounter = 0;
            isJumping = false;
        }





    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        
    }

    public bool IsGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }


    private void Jump(InputAction.CallbackContext context)
    {
        jumpBufferCounter = jumpBufferTime;
        if(coyoteTimeCounter > 0f)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }
        else if(jumpCount < maxJumpCount)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
    private void Dash(InputAction.CallbackContext context)
    {
        if(canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Flip(){
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        
        anim.SetBool("isrolling", true);
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("isrolling", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
