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
            Invoke("CompleteLevel", 2.5f);
        }
    }
    private void CompleteLevel()
    {
        //SceneManager.LoadScene("End Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
