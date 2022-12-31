using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Canvas canvas;

    private int swordCost = 3;
    private int bowCost = 6;
    private int spearCost = 9;

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
        if (GlobalReferenceManager.ItemCollectorScript.AmountOfCoins >= swordCost)
        {
            GlobalReferenceManager.ItemCollectorScript.AmountOfCoins -= swordCost;
            GlobalReferenceManager.PlayerScript.UpdateSpriteState(PlayerWeaponState.Sword);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }

    public void EquipBow()
    {
        if (GlobalReferenceManager.ItemCollectorScript.AmountOfCoins >= bowCost)
        {
            GlobalReferenceManager.ItemCollectorScript.AmountOfCoins -= bowCost;
            GlobalReferenceManager.PlayerScript.UpdateSpriteState(PlayerWeaponState.Bow);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }

    public void EquipSpear()
    {
        if (GlobalReferenceManager.ItemCollectorScript.AmountOfCoins >= spearCost)
        {
            GlobalReferenceManager.ItemCollectorScript.AmountOfCoins -= spearCost;
            GlobalReferenceManager.PlayerScript.UpdateSpriteState(PlayerWeaponState.Spear);
        }
        else
        {
            Debug.Log("You don't have enough coins!");
        }
    }
}
