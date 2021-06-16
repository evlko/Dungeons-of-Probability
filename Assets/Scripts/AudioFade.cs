using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFade {

    public static IEnumerator Fade(AudioSource audioSource, float duration, float targetVolume, Music music)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        music.inFade = false;
        yield break;
    }
}