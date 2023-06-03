using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("Ground collision Params")]
    [SerializeField] protected float groundCheckDistance = 0.2f;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected Transform groundCheck;
    
    protected bool isGrounded;

    protected int faceDir = 1;
    protected bool isFacingRight = true;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }


    protected virtual void Update()
    {
        CheckIfGrounded();
    }

    protected virtual void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
    }

    protected virtual void FlipSprite()
    {
        faceDir = faceDir * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
}
