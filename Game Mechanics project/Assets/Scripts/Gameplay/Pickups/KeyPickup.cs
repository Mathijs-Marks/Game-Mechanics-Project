using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private int keysReceived = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CharacterEvents.keysReceived.Invoke(keysReceived);
            Destroy(gameObject);
        }
    }
}
