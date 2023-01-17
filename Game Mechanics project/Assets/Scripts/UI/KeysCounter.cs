using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysCounter : MonoBehaviour
{
    public int AmountOfKeys
    {
        get { return amountOfKeys; }
        set { amountOfKeys = value; }
    }

    [SerializeField] private int amountOfKeys;
    [SerializeField] TextMeshProUGUI keysText;

    private void OnEnable()
    {
        CharacterEvents.keysReceived += OnKeyReceived;
        CharacterEvents.keysLost += OnKeyLost;
    }

    private void OnDisable()
    {
        CharacterEvents.keysReceived -= OnKeyReceived;
        CharacterEvents.keysLost -= OnKeyLost;
    }

    public void OnKeyReceived(int receivedKeys)
    {
        amountOfKeys += receivedKeys;
        keysText.text = "Keys: " + amountOfKeys;
    }

    public void OnKeyLost(int lostKeys)
    {
        amountOfKeys -= lostKeys;
        keysText.text = "Keys: " + amountOfKeys;
    }
}
