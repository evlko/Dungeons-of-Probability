using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool pauseBehaviour;
    
    void Start()
    {
        
    }

    public void Click(){
        if (pauseBehaviour == true){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }
}
