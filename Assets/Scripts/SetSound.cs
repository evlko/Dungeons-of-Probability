using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeAudioListenerVolume();
    }

    public void ChangeAudioListenerVolume()
    {
        int newVolume = PlayerPrefs.GetInt("Volume");
        AudioListener.volume = newVolume;
    }
}
