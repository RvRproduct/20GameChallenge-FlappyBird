using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip bonkSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = scoreSound;
    }

    public void PlayScore()
    {
        if (audioSource.clip != scoreSound)
        {
            audioSource.clip = scoreSound;
        }

        audioSource.Play();
    }

    public void PlayBonk()
    {
        if (audioSource.clip != bonkSound)
        {
            audioSource.clip = bonkSound;
        }

        audioSource.Play();
    }
}
