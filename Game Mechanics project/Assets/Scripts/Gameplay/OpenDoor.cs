using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private List<Collider2D> doorColliders;
    [SerializeField] private int keysLost = 1;
    [SerializeField] private KeysCounter keysCounter;
    [SerializeField] private Sprite doorNewSprite;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (keysCounter.AmountOfKeys > 0)
        {
            CharacterEvents.keysLost.Invoke(keysLost);

            renderer.sprite = doorNewSprite;
            for (int i = 0; i < doorColliders.Count; i++)
            {
                doorColliders[i].enabled = false;
            }
        }
    }
}
