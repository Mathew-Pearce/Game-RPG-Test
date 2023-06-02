using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    private Animator animator;
    private Rigidbody2D rigidBoddy2D;
    private float xInput;

    void Start()
    {
        rigidBoddy2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

  
    void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            HandleJump();
        }
        //xMovement control
        xInput = Input.GetAxisRaw("Horizontal");
        HandleMovement();
    }

    private void HandleJump()
    {
        rigidBoddy2D.velocity = new Vector2(rigidBoddy2D.velocity.x, jumpForce);
    }

    private void HandleMovement()
    {
        
        rigidBoddy2D.velocity = new Vector2(xInput * walkSpeed, rigidBoddy2D.velocity.y);
        
    }

    private void HandleAnimation()
    {
        bool isMoving = rigidBoddy2D.velocity.x != 0;
        animator.SetBool("isMoving", isMoving);
    }


}
