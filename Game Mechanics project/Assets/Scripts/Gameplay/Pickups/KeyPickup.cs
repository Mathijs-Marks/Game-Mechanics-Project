using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPickup : MonoBehaviour
{
    public UnityEvent<int> amountOfKeysChanged;

    public int AmountOfKeys
    {
        get { return amountOfKeys; }
        set { amountOfKeys = value; }
    }

    [SerializeField] private int amountOfKeys;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            amountOfKeys++;
            amountOfKeysChanged.Invoke(AmountOfKeys);
            Destroy(gameObject);
        }
    }
}
