using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerWeaponState WeaponState
    {
        get { return weaponState; }
        set { weaponState = value; }
    }

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

    public int HighestLevelCompleted
    {
        get { return highestLevelCompleted; }
        set { highestLevelCompleted = value; }
    }

    // Static reference
    public static GameManager Instance;

    // Data to persist
    private int lives;
    private int coins;
    private int keys;
    private int highestLevelCompleted;
    private PlayerWeaponState weaponState;

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

    private void OnEnable()
    {
        CharacterEvents.chooseLevel += LoadLevel;
    }

    private void OnDisable() 
    {
        CharacterEvents.chooseLevel -= LoadLevel;
    }

    private void Update()
    {
        if (lives == 0)
        {
            //GameOver();
            //TODO: When using invoke, both restartlevel and gameover try to execute after 3 lives lost.
            // This results in first the level restarting, then the player being set back to level 1, then infinitely loading level 1
            lives = 3;
            Invoke("GameOver", 2f);
        }
    }

    private void GameOver()
    {
        Debug.Log("You lost all lives!");
        SceneManager.LoadScene("Level 1");
    }

    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
