using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorNextLevel : MonoBehaviour
{
    [SerializeField] private int keysLost = 1;
    [SerializeField] private KeysCounter keysCounter;
    private bool levelCompleted = false;
    [SerializeField] private Sprite doorNewSprite;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (keysCounter.AmountOfKeys > 0 && !levelCompleted)
        {
            CharacterEvents.keysLost.Invoke(keysLost);
            renderer.sprite = doorNewSprite;
            levelCompleted = true;
            GameManager.Instance.HighestLevelCompleted++;
            Invoke("CompleteLevel", 2f);
        }
    }
    private void CompleteLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            CharacterEvents.chooseLevel.Invoke("End Scene");
        }
        else
        {
        CharacterEvents.chooseLevel.Invoke("Blacksmith");
        }
    }
}
