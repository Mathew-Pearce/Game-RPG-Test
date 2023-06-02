using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBoddy2D;
    
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    private float xInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidBoddy2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
        }
        xInput = Input.GetAxisRaw("Horizontal");
        rigidBoddy2D.velocity = new Vector2(xInput * walkSpeed, rigidBoddy2D.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rigidBoddy2D.velocity = new Vector2(rigidBoddy2D.velocity.x, jumpForce);
        }
    }
}
