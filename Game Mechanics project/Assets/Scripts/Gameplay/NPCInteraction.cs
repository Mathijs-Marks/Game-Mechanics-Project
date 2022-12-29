using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Player player; // GetComponent doesn't want to work for the script, so we serialize it.

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
        player.UpdateSpriteState(PlayerWeaponState.Sword);
    }

    public void EquipBow()
    {
        player.UpdateSpriteState(PlayerWeaponState.Bow);
    }

    public void EquipSpear()
    {
        player.UpdateSpriteState(PlayerWeaponState.Spear);
    }
}
