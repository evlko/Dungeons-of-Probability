using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Win;
    public AudioClip Fail;
    public AudioClip EnemyHit;
    public AudioClip PlayerHit;
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

    public void PlaySound(AudioClip clipPlay)
    {
        audioSource.clip = clipPlay;
        audioSource.Play();
    }
}
