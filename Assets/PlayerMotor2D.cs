using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor2D : MonoBehaviour
{

    [Header("Components")]
    public Rigidbody2D rb;
    
    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    
    [Header("Wall Check")]
    public BoxCollider2D leftWallCheck;
    public BoxCollider2D rightWallCheck;
    public float maximumWallFallSpeed;
    public Vector2 wallJumpForce;
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.2f;
    
    [Header("Dash")]
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    
    [SerializeField] private Vector2 moveInput;
    private bool isGrounded;
    private float lastTimeOnGround;
    private bool isDashing;
    private float lastDashTime;
    private bool canDash = true;
    private bool canMove = true;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (isGrounded)
        {
            lastTimeOnGround = Time.time;
            canDash = true;
        }

        if (IsTouchingLeftWall() && moveInput.x < -0.4 || 
            IsTouchingRightWall() && moveInput.x > 0.4)
        {
            if (rb.linearVelocity.y < -maximumWallFallSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maximumWallFallSpeed);
            }
            
        }

        if (!isDashing && canMove)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (Time.time - lastTimeOnGround < coyoteTime && !isDashing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (IsTouchingWalls())
        {
            if (IsTouchingLeftWall())
            {
                StartCoroutine(MaintainVelocity(wallJumpForce, 0.1f));
                StartCoroutine(BlockMovement(0.3f));
            }
            else
            {
                Vector2 velocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
                StartCoroutine(MaintainVelocity(velocity, 0.1f));
                StartCoroutine(BlockMovement(0.3f));
            }
        }
    }
    
    private bool IsTouchingWalls()
    {
        return IsTouchingLeftWall() || IsTouchingRightWall();
    }
    
    private bool IsTouchingLeftWall()
    {
        return leftWallCheck.IsTouchingLayers(groundLayer);
    }
    
    private bool IsTouchingRightWall()
    {
        return rightWallCheck.IsTouchingLayers(groundLayer);
    }

    void OnDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && !isDashing && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 dashVelocity = moveInput * dashForce;
        lastDashTime = Time.time;
        
        yield return MaintainVelocity(dashVelocity, dashDuration);
        
        rb.gravityScale = originalGravity;
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, 0);
        isDashing = false;
    }
    
    IEnumerator MaintainVelocity(Vector2 velocity, float duration)
    {
        float time = Time.time;
        while (Time.time < time + duration)
        {
            rb.linearVelocity = velocity;
            yield return null;
        }
    }
    
    IEnumerator BlockMovement(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
