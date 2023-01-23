using SensorToolkit.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool HasTarget
    {
        get { return hasTarget; }
        set
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

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    [SerializeField] private Transform playerTransform;
    [SerializeField] private BossDetectPlayer detectPlayer;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int randomAmountOfCoins;

    private DamageController damageController;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFlipped = false;
    private bool hasTarget = false;

    private void OnEnable()
    {
        damageController.damageableDeath.AddListener(OnDeath);
    }

    private void Awake()
    {
        damageController = GetComponent<DamageController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        randomAmountOfCoins = Random.Range(2, 5);
    }

    private void Update()
    {
        if (AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;

        if (damageController.Health <= 50)
        {
            animator.SetBool(AnimationStrings.isEnraged, true);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnDeath()
    {
        Vector2 trajectory = UnityEngine.Random.insideUnitCircle * 200f;
        for (int i = 0; i < randomAmountOfCoins; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                UnityEngine.Random.Range(-100f, 100f) + trajectory.x, UnityEngine.Random.Range(50f, 100f) + trajectory.y));
            Debug.Log("Coins spawned: " + randomAmountOfCoins);
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z = -1f;

        if (transform.position.x > playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
