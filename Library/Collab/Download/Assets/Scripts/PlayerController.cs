using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public LayerMask WhatIsGround;
    public Transform groundCheck;
    public Transform WallCheck;
    public GameObject karakter;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    private float movementInputDirection;
    private float JumpTimer;

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
    public float jumptimerSet = 0.15f;

    private bool isFacingRight = true;
    private bool lifeStat;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isAttemtingToJump;
    private bool canNormalJump;
    private bool canWallJump;

    private int amountOfJumpLeft;
    private int facingDirection = 1;
    public int amountOfJumps = 1;


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
        CheckJump();
        Die();
    }

    private void CheckSurrounding()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        isTouchingWall = Physics2D.Raycast(WallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
    }
    private void FixedUpdate()

    {
        ApplyMovement();
        CheckSurrounding();
    }

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

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }


    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpLeft > 0 && isTouchingWall))
            {
                NormalJump();

            }
            else
            {
                JumpTimer = jumptimerSet;
                isAttemtingToJump = true;
            }
        }

        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varibleJumpHeightMultiplier);
        }

    }

    public void CheckJump()
    {
        if (JumpTimer > 0)
        {
            //walljump
            if (!isGrounded && isTouchingWall && movementInputDirection != facingDirection && movementInputDirection == facingDirection)
            {
                WallJump();
            }
            else if (isGrounded && !isWallSliding)
            {
                NormalJump();
            }
        }
        if (isAttemtingToJump)
        {
            JumpTimer -= Time.deltaTime;
        }
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpLeft--;
            JumpTimer = 0;
            isAttemtingToJump = false;
        }
    }

    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpLeft = amountOfJumps;
            amountOfJumpLeft--;
            Vector2 forceToAdd = new Vector2(WallHopForce * wallJumpDirection.x * movementInputDirection, WallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            JumpTimer = 0;
            isAttemtingToJump = false;
        }
    }

    private void CheckIfCanJump()
    {
        if ((isGrounded || isWallSliding) && rb.velocity.y <= 0.01f)
        {
            amountOfJumpLeft = amountOfJumps;

        }
        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputDirection == facingDirection)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    public void Die()
    {
        Debug.Log("öldü");
    }
    private void Flip()
    {
        if (!isWallSliding)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + wallCheckDistance, WallCheck.position.y, WallCheck.position.z));
    }

}