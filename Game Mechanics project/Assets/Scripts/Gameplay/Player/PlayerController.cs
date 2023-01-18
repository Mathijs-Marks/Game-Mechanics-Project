using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
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
            else
            {
                // Movement locked.
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

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    public bool IsAlive { get { return animator.GetBool(AnimationStrings.isAlive); } }

    public float RollCooldown
    {
        get { return animator.GetFloat(AnimationStrings.rollCooldown); }
        private set { animator.SetFloat(AnimationStrings.rollCooldown, Mathf.Max(value, 0)); }
    }

    private bool isFacingRight = true;

    //private CapsuleCollider2D collider;
    private Rigidbody2D rb;

    private Animator animator;
    [SerializeField] private RuntimeAnimatorController[] controller;


    private PlayerAnimationState animationState;

    private DamageController damageController;
    private CheckSurfaces checkSurfaces;

    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private Vector2 rollForce = Vector2.zero;
    private Vector2 moveInput;

    private void Awake()
    {
        GlobalReferenceManager.PlayerScript = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageController = GetComponent<DamageController>();
        checkSurfaces = GetComponent<CheckSurfaces>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RollCooldown > 0)
            RollCooldown -= Time.deltaTime;

        if (RollCooldown > 0.5)
        {
            damageController.IsInvincible = true;
        }
        else
        {
            damageController.IsInvincible = false;
        }
    }

    private void FixedUpdate()
    {
        if(!damageController.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        UpdateAnimationState();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        } 
        else
        {
            IsMoving = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO: Check if alive as well.
        if (context.started && checkSurfaces.IsGrounded && CanMove)
        {
            //jumpSoundEffect.Play();
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && checkSurfaces.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.rollTrigger);
            Vector2 rollDirection = transform.localScale.x > 0 ? rollForce : new Vector2(-rollForce.x, rollForce.y);
            rb.velocity = rollDirection;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
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

        if (rb.velocity.y > .1f /*&& checkSurfaces.IsGrounded == false*/)
        {
            animationState = PlayerAnimationState.Jumping;
        }
        else if (rb.velocity.y < -.1f && checkSurfaces.IsGrounded == false)
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
