using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthRestore = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DamageController damageable = collision.GetComponent<DamageController>();

            if (damageable && damageable.Health < damageable.MaxHealth)
            {
                bool wasHealed = damageable.Heal(healthRestore);
                GameManager.Instance.CurrentHealth = damageable.Health;

                if (wasHealed)
                    Destroy(gameObject);
            }
        }
    }
}
