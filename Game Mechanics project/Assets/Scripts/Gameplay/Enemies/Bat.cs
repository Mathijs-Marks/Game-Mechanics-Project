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

    [SerializeField] private bool hasTarget = false;
    [SerializeField] private DetectionZone attackDetectionZone;
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackDetectionZone.detectedColliders.Count > 0;
    }
}
