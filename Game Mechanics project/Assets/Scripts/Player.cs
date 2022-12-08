using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerState state;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    private float movementX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
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
}
