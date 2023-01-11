using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public bool HasTarget
    {
        get { return hasTarget; }
        private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    [SerializeField] private bool hasTarget = false;
    [SerializeField] private DetectionZone attackDetectionZone;
    [SerializeField] private float flyingSpeed = 2f;
    [SerializeField] private float waypointReachedDistance = 0.1f;
    [SerializeField] private Collider2D deathCollider;
    [SerializeField] private List<Transform> waypoints;

    private int waypointIndex = 0;
    private Transform nextWaypoint;

    private Animator animator;
    private Rigidbody2D rb;
    private DamageController damageable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<DamageController>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointIndex];
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else 
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            
        }
    }

    void Update()
    {
        HasTarget = attackDetectionZone.detectedColliders.Count > 0;
    }

    private void Flight()
    {
        // Fly to the next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position- transform.position).normalized;

        // Check if we have reached the waypoint already
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flyingSpeed;
        UpdateDirection();

        // See if we need to switch waypoints
        if (distance <= waypointReachedDistance)
        {
            // Switch to next waypoint
            waypointIndex++;

            if (waypointIndex >= waypoints.Count)
            {
                // Loop back to original waypoint
                waypointIndex = 0;
            }

            nextWaypoint = waypoints[waypointIndex];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            // Facing the right
            if (rb.velocity.x < 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
        else
        {
            // Facing the left
            if (rb.velocity.x > 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }

    public void OnDeath()
    {
        // It's dead, so it falls to the ground
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
