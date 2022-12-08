using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerState state;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    private float movementX;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(0,jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (movementX > 0f)
        {
            state = PlayerState.Running;
            sprite.flipX = false;
        }
        else if (movementX < 0f)
        {
            state = PlayerState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = PlayerState.Idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = PlayerState.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = PlayerState.Falling;
        }

        animator.SetInteger("state", (int)state);
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
}
