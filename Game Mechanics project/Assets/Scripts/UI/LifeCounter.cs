using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] private List<Image> liveImages;
    private SpriteRenderer renderer;
    [SerializeField] private Sprite emptyLifeSprite;
    [SerializeField] private Sprite fullLifeSprite;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        
        for (int i = 0; i < GameManager.Instance.Lives; i++)
        {
            liveImages[i].sprite = fullLifeSprite;
        }
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        CharacterEvents.characterLosesLife += LoseLife;
    }

    private void OnDisable()
    {
        CharacterEvents.characterLosesLife -= LoseLife;  
    }

    public void LoseLife()
    {
        GameManager.Instance.Keys = 0;
        GameManager.Instance.Lives--;
        liveImages[GameManager.Instance.Lives].sprite = emptyLifeSprite;
    }

}
