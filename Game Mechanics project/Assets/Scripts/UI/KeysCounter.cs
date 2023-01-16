using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keysText;
    [SerializeField] private KeyPickup key;

    private void OnEnable()
    {
        key.amountOfKeysChanged.AddListener(OnKeyChanged);
    }

    public void OnKeyChanged(int keys)
    {
        keysText.text = "Keys: " + key.AmountOfKeys;
    }
}
