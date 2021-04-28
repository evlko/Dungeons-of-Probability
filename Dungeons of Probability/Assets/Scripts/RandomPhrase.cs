using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPhrase : MonoBehaviour
{
    public string key;
    public int keysCount;
    public Text textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GeneratePhrase(){
        int number = Random.Range(0, keysCount);
        string new_key = key + "_" + number.ToString();
        textDisplay.text = LocalizationManager.instance.GetLocalizedValue(new_key);
    }
}
