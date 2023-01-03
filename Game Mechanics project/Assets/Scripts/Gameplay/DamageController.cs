using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set: " + value);
        }
    }

    private Animator animator;
    
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit = 0;
    [SerializeField] private float invincibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

        Hit(10);
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
}
