using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject potionPrefab;

    private int swordCost = 3;
    private int bowCost = 6;
    private int spearCost = 9;
    private int potionCost = 2;

    private void Awake()
    {
        GlobalReferenceManager.NPCInteractionScript = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        canvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            animator.SetBool("IsInteracting", true);
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            animator.SetBool("IsInteracting", false);
            canvas.enabled = false;
        }
    }

    public void EquipSword()
    {
        if (GameManager.Instance.Coins >= swordCost)
        {
            int updatedCoins = GameManager.Instance.Coins -= swordCost;
            GlobalReferenceManager.ItemCollectorScript.UpdateCoins(updatedCoins);
            GlobalReferenceManager.PlayerScript.UpdateSpriteState(PlayerWeaponState.Sword);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }

    public void EquipBow()
    {
        if (GameManager.Instance.Coins >= bowCost)
        {
            int updatedCoins = GameManager.Instance.Coins -= bowCost;
            GlobalReferenceManager.ItemCollectorScript.UpdateCoins(updatedCoins);
            GlobalReferenceManager.PlayerScript.UpdateSpriteState(PlayerWeaponState.Bow);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }

    public void EquipSpear()
    {
        if (GameManager.Instance.Coins >= spearCost)
        {
            int updatedCoins = GameManager.Instance.Coins -= spearCost;
            GlobalReferenceManager.ItemCollectorScript.UpdateCoins(updatedCoins);
            GlobalReferenceManager.PlayerScript.UpdateSpriteState(PlayerWeaponState.Spear);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }

    public void BuyPotion()
    {
        if (GameManager.Instance.Coins >= potionCost)
        {
            int updatedCoins = GameManager.Instance.Coins -= potionCost;
            GlobalReferenceManager.ItemCollectorScript.UpdateCoins(updatedCoins);
            Instantiate(potionPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }
}
