using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
