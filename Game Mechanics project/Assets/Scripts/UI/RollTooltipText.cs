using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTooltipText : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CharacterEvents.rollTooltip.Invoke(collision.gameObject);
        }
    }
}
