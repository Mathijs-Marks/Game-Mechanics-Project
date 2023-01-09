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

    public bool HasTarget { 
        get { return hasTarget; } 
        private set 
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    public float AttackCooldown 
    { 
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); } 
    }

    [SerializeField] private bool hasTarget = false;

    [SerializeField] private float walkAcceleration = 30f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float walkStopRate = 0.02f;

    private Rigidbody2D rb;
    private WalkableDirection walkDirection;
    private Animator animator;
    private Vector2 walkDirectionVector = Vector2.right;
    private CheckSurfaces checkSurfaces;
    private DamageController damageController;
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private DetectionZone cliffDetectionZone;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent <Animator>();
        checkSurfaces= GetComponent<CheckSurfaces>();
        damageController = GetComponent<DamageController>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        
        if(AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkSurfaces.IsGrounded && checkSurfaces.IsOnWall)
        {
            FlipDirection();
        }

        if (!damageController.LockVelocity)
        {
            if (CanMove)
            {
                // Accelerate towards max speed
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            }
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
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

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (checkSurfaces.IsGrounded)
        {
            FlipDirection();
        }
    }
}
