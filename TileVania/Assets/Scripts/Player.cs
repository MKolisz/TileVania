using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config   
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    //State
    bool isAlive=true;

    //Cache
    Rigidbody2D myRigidbody2D;
    Collider2D myCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        var xMove = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(xMove, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(xMove) > Mathf.Epsilon;
        GetComponent<Animator>().SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody2D.velocity += jumpVelocityToAdd;

        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x),1f);
        }
    }
}
