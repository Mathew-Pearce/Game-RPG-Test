using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Movement params")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Player Detection Params")]
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask playerMask;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();   
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * 1.5f * faceDir, rb.velocity.y);
                Debug.Log("I see Youuuu!!!!");
                isAttacking = false;
            }
            else
            {
                Debug.Log("Attacking!!!! " + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)
            FlipSprite();

        Movement();
    }

    private void Movement()
    {
       if (!isAttacking) rb.velocity = new Vector2(moveSpeed * faceDir, rb.velocity.y);
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerDetectionDistance * faceDir, playerMask);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerDetectionDistance * faceDir, transform.position.y));
    }
}
