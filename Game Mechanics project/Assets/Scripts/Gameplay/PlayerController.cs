using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !GlobalReferenceManager.CheckSurfacesScript.IsOnWall)
            {
                return moveSpeed;
            }
            else
            {
                // Idle speed is 0.
                return 0;
            }
        }
    }

    public bool IsFacingRight 
    { 
        get { return isFacingRight; }
        private set 
        {
            if (isFacingRight != value)
            {
                // Flip the horizontal scale to make the player face the opposite direction.
                transform.localScale *= new Vector2(-1, 1);
            }
                isFacingRight = value; 
        } 
    }
    
    private bool isFacingRight = true;

    //private CapsuleCollider2D collider;
    private Rigidbody2D rb;

    private Animator animator;
    [SerializeField] private AnimatorController[] controller;


    private PlayerAnimationState animationState;

    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    private Vector2 moveInput;

    private void Awake()
    {
        GlobalReferenceManager.PlayerScript = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        UpdateAnimationState();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO: Check if alive as well.
        if (context.started && GlobalReferenceManager.CheckSurfacesScript.IsGrounded)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face to the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face to the left
            IsFacingRight = false;
        }
    }

    private void UpdateAnimationState()
    {
        if (moveInput.x > 0f || moveInput.x < 0f)
        {
            animationState = PlayerAnimationState.Running;
        }
        else
        {
            animationState = PlayerAnimationState.Idle;
        }

        if (rb.velocity.y > .1f)
        {
            animationState = PlayerAnimationState.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            animationState = PlayerAnimationState.Falling;
        }

        animator.SetInteger(AnimationStrings.state, (int)animationState);
    }

    

    public void UpdateSpriteState(PlayerWeaponState weaponState)
    {
        if (weaponState == PlayerWeaponState.Sword)
        {
            animator.runtimeAnimatorController = controller[1];
        }
        else if (weaponState == PlayerWeaponState.Bow)
        {
            animator.runtimeAnimatorController = controller[2];
        }
        else if (weaponState == PlayerWeaponState.Spear)
        {
            animator.runtimeAnimatorController = controller[3];
        }
        else
        {
            animator.runtimeAnimatorController = controller[0];
        }
    }
}
