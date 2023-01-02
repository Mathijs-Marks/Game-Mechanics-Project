using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    private BoxCollider2D collider;
    private Rigidbody2D rb;

    private Animator animator;
    [SerializeField] private AnimatorController[] controller;


    private PlayerAnimationState animationState;

    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    private float movementX;
    private Vector2 moveInput;

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Awake()
    {
        GlobalReferenceManager.PlayerScript = this;
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //movementX = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0,jumpForce);
        }

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    private void UpdateAnimationState()
    {
        if (moveInput.x > 0f)
        {
            animationState = PlayerAnimationState.Running;
            sprite.flipX = false;
        }
        else if (moveInput.x < 0f)
        {
            animationState = PlayerAnimationState.Running;
            sprite.flipX = true;
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

        animator.SetInteger("state", (int)animationState);
    }

    private bool IsGrounded()
    {
        /*
         * Create a box around the player, basically like the collision box that the player already has.
         * This box requires some values to be rendered and behave correctly.
         * First, determine the centre of the box. We use the centre of the collider from the player for that.
         * Second, use the size of the player collider box for the size of our box.
         * We don't need an angle, so that gets a 0.
         * Then, move the box downwards just a slight bit (0.1f) so that the box will overlap with stuff that's under it.
         * Now, the box will overlap with other game objects. 
         * Because we only want it to interact with stuff that's the ground, we add the "jumpableGround" layer to the box.
         * Now this method will return a true as soon as the BoxCast overlaps with something with the layer "jumpableGround"!
         */
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

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