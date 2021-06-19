using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpBook : MonoBehaviour
{
    public List<Image> pages = new List<Image>();
    int currentIndex = 0;

    public void OpenPageByRelativeIndex(int relevantIndex){
        ClosePages();
        pages[currentIndex + relevantIndex].gameObject.SetActive(true);
        currentIndex += relevantIndex;
    }

    public void OpenPageByIndex(int index){
        ClosePages();
        pages[index].gameObject.SetActive(true);
        currentIndex = index;
    }

    void ClosePages(){
        for (int i = 0; i < pages.Count; i++){
            pages[i].gameObject.SetActive(false);
        }
    }
}
