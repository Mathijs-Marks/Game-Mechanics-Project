using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSurfaces : MonoBehaviour
{
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set 
        { 
            isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    public bool IsOnWall
    {
        get { return isOnWall; }
        private set
        {
            isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    public bool IsOnCeiling
    {
        get { return isOnCeiling; }
        private set
        {
            isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isOnWall;
    [SerializeField] private bool isOnCeiling;

    private CapsuleCollider2D touchingCollider;
    private Animator animator;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    [SerializeField] private ContactFilter2D castFilter;

    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float ceilingDistance = 0.05f;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private void Awake()
    {
        GlobalReferenceManager.CheckSurfacesScript = this;
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator= GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCollider.Cast(Vector2.down, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
