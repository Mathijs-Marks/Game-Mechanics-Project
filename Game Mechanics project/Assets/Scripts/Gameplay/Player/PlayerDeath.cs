using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private DamageController damageable;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Awake()
    {
        damageable = GetComponent<DamageController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        damageable.IsAlive = false;
        animator.SetTrigger(AnimationStrings.deathTrigger); // Set trigger for animator, which then executes RestartLevel()
        //rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
