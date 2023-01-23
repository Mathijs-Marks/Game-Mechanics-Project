using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDetectPlayer : MonoBehaviour
{
    [SerializeField] private Boss boss;
    public UnityEvent getTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boss.HasTarget = true;
        }
    }
}
