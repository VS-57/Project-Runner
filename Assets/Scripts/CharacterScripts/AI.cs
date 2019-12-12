using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public LayerMask WhatIsGround;
    public Transform groundCheck;
    public Transform nextGroundCheck;
    public Transform WallCheck;
    public Transform nextWallCheck;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    private float movementInputDirection;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float movementSpeed = 10.0f;
    public float jumpForce = 8.0f;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float varibleJumpHeightMultiplier = 0.5f;
    public float WallHopForce;
    public float WallJumpForce;

    private bool isFacingRight = true;
    private bool isWalking;
    public bool isGrounded;
    public bool NextGrounded;
    private bool canJump;
    private bool isTouchingWall;
    private bool NextTouchWall;
    private bool isWallSliding;
    private bool crouch = false;

    private int amountOfJumpLeft;
    private int facingDirection = 1;
    public int amountOfJumps = 1;

    public bool boxtrigger;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
    }
    // Nesneye temas edip etmediğini kontrol etme
    private void CheckSurrounding()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        NextGrounded = Physics2D.OverlapCircle(nextGroundCheck.position, groundCheckRadius, WhatIsGround);

        isTouchingWall = Physics2D.Raycast(WallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
        NextTouchWall = Physics2D.Raycast(nextWallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
    }
    private void FixedUpdate()

    {
        ApplyMovement();
        CheckSurrounding();
    }
    // Karakterin baktığı yönü kontrol etme
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
        if (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    // Animasyonları Input'lara göre güncelleme
    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("Crouch", crouch);
    }

    // Klavye giriş tuşlarını kontrol etme
    private void CheckInput()
    {
        if (nextGroundCheck && !isTouchingWall )
        {
            movementInputDirection = 1;
        }
        else if (nextGroundCheck && isTouchingWall)
        {
            movementInputDirection = -1;
        }

        if (!NextGrounded && isGrounded)
        {
            Jump();
        }

        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varibleJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Submit"))
        {
            boxtrigger = true;
        }

    }

    public void Jump()
    {
        
        if (isWallSliding && movementInputDirection == facingDirection && canJump)
        {
            isWallSliding = false;
            amountOfJumpLeft--;
            Vector2 forceToAdd = new Vector2(WallHopForce * wallHopDirection.x * facingDirection, WallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);

        }
        else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpLeft--;
            Vector2 forceToAdd = new Vector2(WallJumpForce * wallJumpDirection.x * movementInputDirection, WallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }
    // Zıplamayı kontrol etme (yerdeyken veya duvarda kayarken)
    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0) || isWallSliding)
        {
            amountOfJumpLeft = amountOfJumps;

        }
        if (amountOfJumpLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }
    // Duvarda kaymayı kontrol etme
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 && !crouch)
        {
            isWallSliding = true;
            if (movementInputDirection == 1)
            {
                Jump();
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varibleJumpHeightMultiplier);
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                Jump();
                rb.velocity = new Vector2(rb.velocity.x * varibleJumpHeightMultiplier, rb.velocity.y);
            }

        }
        else
        {
            isWallSliding = false;
        }
    }
    // Yürüme kodu
    private void ApplyMovement()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }


        }
        else if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    void Die()
    {
        gameObject.SetActive(false);

    }
    // Karakterin baktığı yönü ayarlama
    private void Flip()
    {
        if (!isWallSliding)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
    // Kontrolcü oluşturma
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(nextGroundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + wallCheckDistance, WallCheck.position.y, WallCheck.position.z));
        Gizmos.DrawLine(WallCheck.position, new Vector3(nextWallCheck.position.x + wallCheckDistance, nextWallCheck.position.y, nextWallCheck.position.z));
    }

}
