using UnityEngine;
 
public class Music : MonoBehaviour
{
    AudioSource _audioSource;
    float duration;
    public bool inFade;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        duration = _audioSource.clip.length;
        //StartCoroutine(AudioFade.Fade(_audioSource, 10f, 1));
    }
 
    private void FixedUpdate() {
        float currentTime = _audioSource.time;
        if (inFade == false && currentTime < 10f){
            inFade = true;
            StartCoroutine(AudioFade.Fade(_audioSource, 10f, 0.9f, GetComponent<Music>()));
        }
        else if (inFade == false && duration - 10 < currentTime){
            inFade = true;
            StartCoroutine(AudioFade.Fade(_audioSource, 10f, 0, GetComponent<Music>()));
        }
    }
}