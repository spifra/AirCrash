using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnGameOver()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    public void OnRevival()
    {
        audioSource.clip = clips[1];
        audioSource.Play();
    }
}
