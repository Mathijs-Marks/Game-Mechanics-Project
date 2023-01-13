using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if the target can be hit
        DamageController damageable = collision.GetComponent<DamageController>();

        if (damageable != null)
        {
            // If parent is facing the left by localscale, the knockback x flips its value to face the left as well.
            Vector2 deliveredKnockback = transform.parent.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Hit the target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (gotHit)
                Debug.Log(collision.name + " hit for: " + attackDamage);
        }
    }
}
