using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    
    public void SetData()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("Healths", 3);
        PlayerPrefs.SetInt("EnemyHealths", 3);
        PlayerPrefsX.SetIntArray("Questions", new int[11]);
        PlayerPrefs.SetInt("CameraPoint", 0);
        PlayerPrefsX.SetIntArray("HeroesNames", new int[4]);
        PlayerPrefsX.SetIntArray("HeroesLevels", new int[4]);
    }
}
