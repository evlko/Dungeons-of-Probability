using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChecker : MonoBehaviour
{
    public bool status;
    void Start()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            gameObject.SetActive(status);
        }
    }
}
