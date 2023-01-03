using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CheckSurfaces))]
public class Skeleton : MonoBehaviour
{
    public WalkableDirection WalkDirection
    {
        get { return walkDirection; }
        set 
        { 
            if (walkDirection != value)
            {
                // Flip direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            
            walkDirection = value; 
        }
    }

    [SerializeField] private float walkSpeed = 3f;

    private Rigidbody2D rb;
    private WalkableDirection walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    private CheckSurfaces checkSurfaces;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        checkSurfaces= GetComponent<CheckSurfaces>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkSurfaces.IsGrounded && checkSurfaces.IsOnWall)
        {
            FlipDirection();
        }

        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walk direction is not set to legal values of right or left!");
        }
    }
}
