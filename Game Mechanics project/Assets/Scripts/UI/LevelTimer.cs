using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private bool timerRunning = false;
    [SerializeField] private float timeRemaining;
    [SerializeField] private LifeCounter lifeCounter;
    [SerializeField] DamageController damageController;

    private void Start()
    {
        timerRunning = true;
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                LoseLife();
            }
        }
    }

    private void LoseLife()
    {
        timeRemaining = 0;
        timerRunning = false;
        damageController.IsAlive = false;
    }

    private void DisplayTime(float timeRemaining)
    {
        timeRemaining += 1; // Used to keep showing a 1 until the timer actually ends up at 0, like with an actual timer on your phone.
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
