using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip bossMusic;
    private float fadeDuration = 3f;
    private float fadeTargetVolume = 0f;
    private float originalVolume = 0.4f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FadeAudioSource.StartFade(source, fadeDuration, fadeTargetVolume));
            Invoke("StartNewClip", 3f);
        }
    }

    private void StartNewClip()
    {
        if (source.clip == backgroundMusic)
        {
            source.clip = bossMusic;
            source.volume = originalVolume;
            source.Play();
        }
        else
        {
            source.clip = backgroundMusic;
            source.volume = originalVolume;
            source.Play();
        }
    }
}
