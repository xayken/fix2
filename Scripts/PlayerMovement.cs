using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private float movementInput;
    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    public bool isFacingRight = true;
    private bool isGrounded;
    private bool canNormalJump = false;
    private bool canFlip = true;
    [SerializeField]private bool isCrouched = false;
    private Animator anim;
    private Rigidbody2D rb;


    [SerializeField] private float movementSpeed= 10f;
    [SerializeField] private float crouchMoveSpeed = 5f;
    [SerializeField] private int amountOfJumps = 1;
    [SerializeField] private float jumpForce = 16.0f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        CheckInput();
        CheckDirection();
        CheckIfCanJump();
        Crouch();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckGround();
    }

    private void CheckInput()
    {
        movementInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && canNormalJump)
        {
            NormalJump();
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        canNormalJump = amountOfJumpsLeft > 0;
    }

    private void CheckDirection()
    {
        if (isFacingRight && movementInput < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInput > 0)
        {
            Flip();
        }
    }

    private void ApplyMovement()
    {
        if (isGrounded)
        {
            if (isCrouched)
            {
                rb.velocity = new Vector2(crouchMoveSpeed * movementInput, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(movementSpeed * movementInput, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(movementSpeed * movementInput * 0.85f, rb.velocity.y);
        }
    }


    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void NormalJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        amountOfJumpsLeft--;
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.C) && isGrounded)
        {
            isCrouched = true;


            





        }
        else
        {
            isCrouched = false;

            


        }

        anim.SetBool("isCrouched", isCrouched);

    }

    private void Flip()
    {
        if (canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}