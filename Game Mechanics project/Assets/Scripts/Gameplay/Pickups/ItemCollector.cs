using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int AmountOfCoins
    {
        get { return amountOfCoins; }
        set { amountOfCoins = value; }
    }

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private AudioSource collectSoundEffect;
    
    private int amountOfCoins = 0;

    private void Awake()
    {
        GlobalReferenceManager.ItemCollectorScript = this;
        UpdateCoins(GameManager.Instance.Coins);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision);
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            PlayCollectSoundEffect();
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
        Destroy(collision.gameObject.transform.parent.gameObject);
        GameManager.Instance.Coins++;
        UpdateCoins(GameManager.Instance.Coins);
    }

    public void UpdateCoins(int amountOfCoins)
    {
        coinsText.text = "Coins: " + amountOfCoins;
    }

    private void CollectWeapon(Collider2D collision, PlayerWeaponState weaponState)
    {
        collectSoundEffect.Play();
        Destroy(collision.gameObject);
        GlobalReferenceManager.PlayerScript.UpdateSpriteState(weaponState);

    }

    private void PlayCollectSoundEffect()
    {
        collectSoundEffect.Play();
    }
}
