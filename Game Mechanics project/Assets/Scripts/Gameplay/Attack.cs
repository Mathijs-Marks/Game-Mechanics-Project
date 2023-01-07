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
            // Flip the knockback direction if the direction of the object is also flipped
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Hit the target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (gotHit)
                Debug.Log(collision.name + " hit for: " + attackDamage);
        }
    }
}
