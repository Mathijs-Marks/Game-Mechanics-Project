using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNextLevel : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        canvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            canvas.enabled = false;
        }
    }

    public void LoadLevel1()
    {
        CharacterEvents.chooseLevel.Invoke("Level 1");
    }

    public void LoadLevel2()
    {
        if (GameManager.Instance.HighestLevelCompleted >= 1)
        {
            CharacterEvents.chooseLevel.Invoke("Level 2");
        }
    }

    public void LoadLevel3()
    {
        if (GameManager.Instance.HighestLevelCompleted >= 2)
        {
            CharacterEvents.chooseLevel.Invoke("Level 3");
        }
    }
}
