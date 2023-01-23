using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private Vector2 moveSpeed = new Vector2(3f, 0);
    [SerializeField] private Vector2 knockback = new Vector2(0, 0);

    [SerializeField] private float despawnTimer = 2f;
    private float timeElapsed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // If you want the projectile to be effected by gravity by default, make it dynamic mode in rigidbody.
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= despawnTimer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageController damageable = collision.GetComponent<DamageController>();

        if (damageable != null)
        {
            // If parent is facing the left by localscale, the knockback x flips its value to face the left as well.
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            if (gotHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
