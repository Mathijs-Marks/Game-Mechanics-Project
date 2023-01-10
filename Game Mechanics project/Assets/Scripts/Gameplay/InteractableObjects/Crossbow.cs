using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    // TODO: Actually an enemy. Shoots arrows at player.
    public bool HasTarget
    {
        get { return hasTarget; }
        private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); }
    }

    [SerializeField] private bool hasTarget = false;

    private Animator animator;
    private DamageController damageController;
    [SerializeField] private DetectionZone attackZone;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageController = GetComponent<DamageController>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCooldown> 0)
            AttackCooldown -= Time.deltaTime;
    }

    public void OnAttack()
    {
        // TODO: check if you need this.
    }
}
