using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private AudioSource collectSoundEffect;

    private Player player;
    
    private int amountOfCoins = 0;

    private void Awake()
    {
        player= GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision);
        }
        else if (collision.gameObject.name == "Sword")
        {
            CollectWeapon(collision, PlayerWeaponState.Sword);
        }
        else if (collision.gameObject.name == "Bow")
        {
            CollectWeapon(collision, PlayerWeaponState.Bow);
        }
        else if (collision.gameObject.name == "Spear")
        {
            CollectWeapon(collision, PlayerWeaponState.Spear);
        }
    }

    private void CollectCoin(Collider2D collision)
    {
        collectSoundEffect.Play();
        Destroy(collision.gameObject);
        amountOfCoins++;
        coinsText.text = "Coins: " + amountOfCoins;
    }

    private void CollectWeapon(Collider2D collision, PlayerWeaponState weaponState)
    {
        collectSoundEffect.Play();
        Destroy(collision.gameObject);
        player.UpdateSpriteState(weaponState);

    }
}
