using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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

    private float xInput;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        HandleInput();
        HandleMovement();
        CollisionCheck();
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

    private void HandleMovement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if(dashTimer > 0)
        {
            rb.velocity = new Vector2(faceDir * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * walkSpeed, rb.velocity.y);
        }

        
        FlipController();
    }

    private void HandleAnimation()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("Dashing", dashTimer > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("ComboCounter", comboCounter);

    }


    private void FlipController()
    {
        if(rb.velocity.x > 0 && !isFacingRight) 
            FlipSprite(); 
        else if (rb.velocity.x < 0 && isFacingRight)
            FlipSprite();
    }

    private void HandleTimers()
    {
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
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

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
    }
}
