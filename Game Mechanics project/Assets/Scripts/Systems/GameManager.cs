using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int Keys
    {
        get { return keys; }
        set { keys = value; }
    }

    // Static reference
    public static GameManager Instance;

    // Data to persist
    private int lives;
    private int coins;
    private int keys;

    private void Awake()
    {
        // Persist gameobject over scenes
        DontDestroyOnLoad(gameObject);

        // Check if the instance is null
        if (Instance == null)
        {
            // This instance becomes the single instance available
            Instance = this;
            lives = 3;
        }
        // Otherwise check if the instance is not this one
        else if (Instance != this)
        {
            // In case there is a different instance, destroy this one
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (lives == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("You lost all lives!");
        lives = 3;
        SceneManager.LoadScene("Level 1");
    }
}
