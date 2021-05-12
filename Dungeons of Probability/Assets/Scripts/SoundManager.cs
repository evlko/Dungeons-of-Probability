using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Win;
    public AudioClip Fail;
    public AudioClip EnemyHit;
    public AudioClip PlayerHit;
    public AudioClip[] PlayerSteps;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Playing()
    {
        return audioSource.isPlaying;
    }

    public void PlaySound(AudioClip clipPlay)
    {
        audioSource.clip = clipPlay;
        audioSource.Play();
    }
}
