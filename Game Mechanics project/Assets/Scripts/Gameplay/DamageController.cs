using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageController : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

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
            healthChanged?.Invoke(health, maxHealth);

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

            if (value == false)
            {
                damageableDeath.Invoke();
                CharacterEvents.characterLosesLife.Invoke(gameObject);
            }
        }
    }
    
    // The velocity should not be changed while this is true but needs to be respected by other physics components, like
    // the player controller.
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    public bool IsInvincible 
    { 
        get { return isInvincible; } 
        set { isInvincible = value; }
    }

    public float InvincibilityTime
    {
        get { return invincibilityTime; }
        set { invincibilityTime = value; }
    }

    private Animator animator;
    
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private int armourValue;
    [SerializeField] private BossHealthBar bossHealthBar;

    private float timeSinceHit = 0;
    [SerializeField] private float invincibilityTime = 0.25f;

    private void Awake()
    {
        GlobalReferenceManager.DamageControllerScript = this;
        animator = GetComponent<Animator>();

        if (gameObject.tag == "Player")
        {
            Health = GameManager.Instance.CurrentHealth;
            healthChanged?.Invoke(GameManager.Instance.CurrentHealth, maxHealth);
        }

        if (gameObject.tag == "Boss")
        {
            bossHealthBar.SetMaxHealth(maxHealth);
        }
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
    }

    // Returns whether the damageable component can be hit or not.
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            if (GameManager.Instance.HasArmour)
            {
                int reducedDamage = damage - armourValue;

                Health -= reducedDamage;
                isInvincible = true;

                // Notify other subscribed components that the damageable was hit to handle knockback, etc.
                animator.SetTrigger(AnimationStrings.hitTrigger);
                LockVelocity = true;
                damageableHit?.Invoke(reducedDamage, knockback);
                CharacterEvents.characterDamaged.Invoke(gameObject, reducedDamage);

                return true;
            }
            else
            {
                Health -= damage;

                if (gameObject.tag == "Boss")
                {
                    bossHealthBar.SetHealth(Health);
                }

                isInvincible = true;

                // Notify other subscribed components that the damageable was hit to handle knockback, etc.
                animator.SetTrigger(AnimationStrings.hitTrigger);
                LockVelocity = true;
                damageableHit?.Invoke(damage, knockback);
                CharacterEvents.characterDamaged.Invoke(gameObject, damage);

                return true;
            }
        }
        
        // Unable to be hit.
        return false;
    }

    // Returns whether the character was healed or not
    public bool Heal(int healthRestored)
    {
        if (isAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestored);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
