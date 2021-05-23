using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChecker : MonoBehaviour
{
    [SerializeField]
    private Font font;
    // Start is called before the first frame update
    void Start()
    {
        font.material.mainTexture.filterMode = FilterMode.Point;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        while (!LocalizationManager.instance.GetIsReady())
        {
            yield return null;
        }
        SceneManager.LoadScene("Main Menu");
    }
}
