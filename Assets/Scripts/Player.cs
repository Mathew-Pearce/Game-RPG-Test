using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player control Params")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    [Header("Dash Params")]
    [SerializeField] private float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
   
    private float dashTimer;
    private float dashCooldownTimer;

    [Header("Attack Params")]
    [SerializeField] float comboTimeWindow;
    [SerializeField] float comboTime = 0.3f;
    private bool isAttacking;
    private int comboCounter;
  


    [Header("Ground collision Params")]
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask groundMask;

    private Animator animator;
    private Rigidbody2D rigidBoddy2D;
    private float xInput;
    private int faceDir = 1;
    private bool isFacingRight = true;
    private bool isGrounded;

    void Start()
    {
        rigidBoddy2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

  
    void Update()
    {
        HandleInput();
        HandleMovement();
        CheckIfGrounded();
        HandleTimers();
        HandleAnimation();
    }


    private void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            HandleJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttack();
        }
    }



    private void DashAbility()
    {
        if(dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTimer = dashDuration;
        }
        
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            rigidBoddy2D.velocity = new Vector2(rigidBoddy2D.velocity.x, jumpForce);
        }

    }

    private void HandleMovement()
    {
        if (isAttacking)
        {
            rigidBoddy2D.velocity = new Vector2(0, 0);
        }
        else if(dashTimer > 0)
        {
            rigidBoddy2D.velocity = new Vector2(faceDir * dashSpeed, 0);
        }
        else
        {
            rigidBoddy2D.velocity = new Vector2(xInput * walkSpeed, rigidBoddy2D.velocity.y);
        }

        
        FlipController();
    }

    private void HandleAnimation()
    {
        bool isMoving = rigidBoddy2D.velocity.x != 0;
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("yVelocity", rigidBoddy2D.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("Dashing", dashTimer > 0);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("ComboCounter", comboCounter);

    }

    private void FlipSprite()
    {
        faceDir = faceDir * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if(rigidBoddy2D.velocity.x > 0 && !isFacingRight) 
            FlipSprite(); 
        else if (rigidBoddy2D.velocity.x < 0 && isFacingRight)
            FlipSprite();
    }

    private void HandleTimers()
    {
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);
    }

    private void StartAttack()
    {
        if (!isGrounded) return;
        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    public void AttackOver()
    {
        isAttacking = false;
        
        comboCounter++;
       
        if (comboCounter > 2) 
        comboCounter = 0;
    }
}
